using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(EnemyBrain))]
public class EnemyController : MonoBehaviour
{

    /*
    move (position)
    shoot (position)

    moving will take the form of hovering a fixed distance above the ground
    shooting will take the form of a short delay, during which the line is broadcast. then after the delay
    it gets bigger and raycast hits damage towards the player, and does a particle affect on whatever object it hits. 
     */
    [HideInInspector]
    public Vector3 targetPosition;
    [HideInInspector]
    public Vector3 shootAt;
    public float chargeDelay;

    Dictionary<string,Action> States;
    public string state;

    public float speed;
    public float hoverHeight;
    float initiateAttackTime;

    LineRenderer lr;

    void Awake()
    {
        lr  = GetComponent<LineRenderer>();
        States= new Dictionary<string,Action>();
        state = "MovingState";
        States["MovingState"] = MovingState;
        States["AttackingState"] = AttackingState;
    }

    public void Attack(Vector3 target)
    {
        if(state != "AttackingState")
        {
            shootAt = target;

            state = "AttackingState";
            initiateAttackTime = Time.time;
            lr.enabled = true;
            lr.startWidth = .1f;
            lr.endWidth = .1f;
            lr.SetPosition(0,transform.position);


            RaycastHit raycastHit;
            Ray r = new Ray(transform.position, target-transform.position);
            //Collider collider = null;
            // if(TryGetComponent<Collider>(out collider))
            //     collider.enabled = false;

            if(Physics.Raycast(r,out raycastHit,50f))
            {
                lr.SetPosition(1,raycastHit.point);   
                //maybe spawn a particle system at the hitpoint    
            }
            else
                lr.SetPosition(1, (target-transform.position).normalized * 50f ) ;

            // if(collider != null)
            //     collider.enabled = true;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        States[state]();
    }

    void MovingState()
    {
        print("Moving");
        targetPosition.y = 0;
        Vector3 myxz = transform.position;
        myxz.y = 0;
        if((targetPosition-myxz).magnitude < .04f)
            {
                GetComponent<EnemyBrain>().IncrementPoint();
            }
        else
            transform.position +=  (targetPosition-myxz).normalized * speed*Time.deltaTime;

        RaycastHit raycastHit;
        Ray r = new Ray(transform.position, Vector3.down);
        if(Physics.Raycast(r,out raycastHit,hoverHeight*1.1f))
        {
            if(raycastHit.distance < hoverHeight)
                transform.position += Vector3.up * (hoverHeight-raycastHit.distance);
        }
        else
            transform.position += Vector3.down * 9.81f * Time.deltaTime;

    }

    void AttackingState()
    {
        if(Time.time  - initiateAttackTime > chargeDelay)
        {
            StartCoroutine("FIREMAHLAZAR");
        }
    }

    IEnumerator FIREMAHLAZAR()
    {
        lr.startWidth = .5f;
        lr.endWidth = .5f;
        yield return new WaitForSeconds(chargeDelay/2);
        lr.enabled = false;
        state = "MovingState";

        
    }



}
