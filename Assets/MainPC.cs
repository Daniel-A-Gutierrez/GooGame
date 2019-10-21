using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class MainPC : MonoBehaviour
{

    public int HP;
    public int MaxHP;
    public LayerMask interactable;
    public LayerMask stick;
    Rigidbody rb;

    public KeyCode release;
    public KeyCode jump;

    public float jumpCooldown;
    public float maxJumpSpeed;
    public float minJumpSpeed;
    public float chargeupTime;
    public float overchargeTime;
    float jumpInitTime;


    public float minTravelBeforeAffix;
    public float minTimeBeforeAffix;

    float traveled;
    Vector3 lastPosition;
    float minTimeBeforeAffixScaled;

    float lastAffixTime;
    Vector3 lastAffixPos;
    bool stickytime;

    [SerializeField]
    string state;
    Dictionary<string, Action> States;

    GameObject attatchedTo;
    GameObject cam;
    Vector3 aimdir;
    Vector3 initialScale;

    public GameObject Cam;
    LaunchArcRenderer arcRenderer;
    GameObject interactingWith;

    public LayerMask heal;
    public LayerMask neutral;
    public LayerMask damage;
    //Inputs inputs;

    public Vector3 exposedVelocity;
    public int collisions = 0;

    int lastHitLayer = 0;
    int healAmount = 0;

    AudioManager audioManager;

    //create States, set default state
    void Awake()
    {
        lastPosition = transform.position;
        initialScale = transform.localScale;
        rb = GetComponent<Rigidbody>();
        States = new Dictionary<string, Action>();
        state = "MovingState"; //stuck on non interactable object
        States["DefaultState"] = DefaultState;
        States["InteractingState"] = InteractingState;
        States["ChargingUpState"] = ChargingUpState;
        States["MovingState"] = MovingState;

        arcRenderer = Cam.GetComponent<LaunchArcRenderer>();
        arcRenderer.show_bar = false;
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        audioManager.player = gameObject;
    }

    //do States[state]
    void Update()
    {
        traveled += (transform.position - lastPosition).magnitude;
        lastPosition = transform.position;
        aimdir = transform.position - Cam.transform.position + Cam.GetComponent<CameraFollow>().offset;
        States[state]();
        //rb.AddForce(Vector3.right);
    }

    //Default State     
    void EnterDefaultState()
    {
        state = "DefaultState";
    }
    //go to moving if release, go to charging up if jump
    void DefaultState()
    {
        if (Input.GetKeyUp(release))
        {
            ExitDefaultState();
            EnterMovingState();
        }
        if (Input.GetKeyDown(jump) && Time.time - jumpInitTime > jumpCooldown)
        {
            ExitDefaultState();
            EnterChargingUpState();
        }
    }

    void ExitDefaultState()
    {

    }

    //Interacting State
    void EnterInteractingState()
    {
        state = "InteractingState";
    }
    void InteractingState()
    {

    }
    void ExitInteractingState()
    {

    }
    //Charging Up State
    void EnterChargingUpState()
    {
        arcRenderer.show_bar = true;
        state = "ChargingUpState";
        jumpInitTime = Time.time;
    }
    //enter moving state if successful jump, enter default if overcharge. 
    void ChargingUpState()
    {
        exposedVelocity = aimdir.normalized *
                Mathf.Lerp(minJumpSpeed, maxJumpSpeed, (Time.time - jumpInitTime) / chargeupTime);
        if (Input.GetKeyUp(jump))
        {
            ExitChargingUpState();
            EnterMovingState();
            Launch();
        }
        if (Time.time - jumpInitTime > overchargeTime)
        {
            ExitChargingUpState();
            EnterDefaultState();
        }
    }

    void Launch()
    {
        float magnitude = Mathf.Lerp(minJumpSpeed, maxJumpSpeed, (Time.time - jumpInitTime) / chargeupTime);
        rb.AddForce(aimdir.normalized * magnitude, ForceMode.VelocityChange);
        minTimeBeforeAffixScaled = minTimeBeforeAffix * magnitude;
        traveled = 0;
    }

    void ExitChargingUpState()
    {
        arcRenderer.show_bar = false;
        exposedVelocity = Vector3.zero;
    }
    //MovingState
    //set kinematic, set no parent, add force, stickytime false, set state 
    void EnterMovingState()
    {
        /*transform.SetParent(null);
        transform.localScale = initialScale;*/
        rb.isKinematic = false;
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
        attatchedTo = null;
        stickytime = false;
        state = "MovingState";
    }
    //wait for stickytime to be true to go to default.
    void MovingState()
    {
        //wait for collision
        if (stickytime)
        {
            ExitMovingState();
            EnterDefaultState();
        }
    }
    //set parent, set kinematic
    void ExitMovingState()
    {
        //on notiification of collision
        /*transform.SetParent(attatchedTo.transform);
        transform.localEulerAngles = Vector3.zero;
        Vector3 i = attatchedTo.transform.localScale;
        transform.localScale=  new Vector3(1/i.x, 1/i.y, 1/i.z);*/
        rb.collisionDetectionMode = CollisionDetectionMode.Discrete;
        rb.isKinematic = true;
        //if the thing is interactable, enter that state. for now we're not doing that.

    }

    void OnCollisionEnter(Collision c)
    {
        audioManager.Play("zapsplat");
        Debug.Log(stickytime);
        collisions++;
        if (c.impulse.sqrMagnitude > 0)
            GetComponent<JigglePhysics>().Squish(c.impulse.magnitude, c.impulse);

        lastHitLayer = c.gameObject.layer;

        if (c.gameObject.GetComponent<HealingItem>() != null)
            healAmount = c.gameObject.GetComponent<HealingItem>().amount;

        if (attatchedTo != c.gameObject)
        {
            attatchedTo = c.gameObject;
        }
        // if( (transform.position - lastAffixPos).magnitude > minTravelBeforeAffix ||
        //  Time.time- lastAffixTime > minTimeBeforeAffix)
        // {
        //     stickytime= true;
        //     attatchedTo = c.gameObject;
        //     lastAffixPos= transform.position;
        //     lastAffixTime = Time.time;
        // }
    }

    void FixedUpdate()
    {
        if (collisions > 0 && !stickytime)
        if (traveled > minTravelBeforeAffix ||
         Time.time - lastAffixTime > minTimeBeforeAffixScaled)
        {
            if ((1 << lastHitLayer & heal) != 0)
                HP = MaxHP;

            if ((1 << lastHitLayer & damage) != 0)
            {
                //c.gameObject.layer = neutral;
                //Debug.Log("damage: " + HP);
                HP--;
                    if (HP == 0)
                    {
                        audioManager.Play("zapsplat3");
                        rb.isKinematic = true;
                        GetComponentInChildren<DieEffect>().Die(transform.position);
                        enabled = false;
                        GetComponentInChildren<MeshRenderer>().enabled = false;
                        SceneTransition.instance.StartTransition(SceneManager.GetActiveScene().name, 60);
                    }
            }

                stickytime = true;
            lastAffixPos = transform.position;
            lastAffixTime = Time.time;
        }
    }

    void OnCollisionExit(Collision c)
    {
        audioManager.Play("zapsplat2");
        collisions--;
    }

    //     void UpdateInputs()
    //     {
    //         inputs = new Inputs(

    //         )
    //     }


    //     //int inputs : 0 = not down, 1 = first frame down, 2 = first or after frame down, 3 = frame up.
    //     struct Inputs
    //     {
    //         int jump;
    //         int release;
    //         float mouseX;
    //         float mouseY;
    //         int forward;
    //         int backward;
    //         int left;
    //         int right;
    //     }
}
