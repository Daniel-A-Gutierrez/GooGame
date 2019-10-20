using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class CameraFollow: MonoBehaviour {

    public GameObject player;
    public float followDistance;
    public float HorizontalSensitivity;
    public float VerticalSensitivity;
    public Vector3 offset;
    Vector3 oldPlayerPosition;
    public KeyCode aim;
    public float aimingFollowDistance;
    public Vector3 aimingOffset;
    float followDistanceO;
    Vector3 offsetO;

    float defaultFOV;
    public float aimingFOV;
    //actually just gonna do something else.

    /*
        predef distance from player, camera transform. 
        rotate by mouse movement
        in player controller interpret forward as camera.forward
    */
    public float horizDir = 0;
    public float vertDir = 0;

    public bool lockCamera = false;

    public void Start()
    {
        defaultFOV = GetComponent<Camera>().fieldOfView;
        offsetO = offset;
        followDistanceO = followDistance;
        oldPlayerPosition = player.transform.position;
        //transform.forward = player.transform.position + offset - transform.position;
        //transform.position = player.transform.position + offset -player.transform.forward*followDistance;
    }

    void LateUpdate()
    {
        if(Input.GetKey(aim))
        {
            offset = aimingOffset;
            followDistance = aimingFollowDistance;
            GetComponent<Camera>().fieldOfView = aimingFOV;
            //GetComponent<LaunchArcRenderer>().show_bar = true;
        }
        else
        {
            offset = offsetO;
            followDistance = followDistanceO;
            GetComponent<Camera>().fieldOfView = defaultFOV;
            //GetComponent<LaunchArcRenderer>().show_bar = false;

        }
    }

    void FixedUpdate () 
    {


        transform.Translate(player.transform.position-oldPlayerPosition , Space.World);
        oldPlayerPosition = player.transform.position;

        if (!lockCamera)
        {
            float dy = -VerticalSensitivity * Input.GetAxis("Mouse Y");
            float dh = HorizontalSensitivity * Input.GetAxis("Mouse X");
            /*transform.RotateAround(player.transform.position,Vector3.up,dh);

            //clamp the y rotation
            bool lookingUp = dy<0;
            bool lookingDown = dy>0;
            if( !((lookingUp & (transform.eulerAngles.x+90)%360 < 10 -dy ) | (lookingDown & (transform.eulerAngles.x+90)%360 >170-dy)))
                transform.RotateAround(player.transform.position,transform.right,dy);

            transform.forward = (player.transform.position+offset-transform.position).normalized;*/

            horizDir -= dh;
            vertDir = Mathf.Clamp(vertDir + dy, -89.9f, 89.9f);
        }

        Vector3 direction = new Vector3(Mathf.Cos(horizDir * Mathf.Deg2Rad) * Mathf.Cos(vertDir * Mathf.Deg2Rad), Mathf.Sin(vertDir * Mathf.Deg2Rad), Mathf.Sin(horizDir * Mathf.Deg2Rad) * Mathf.Cos(vertDir * Mathf.Deg2Rad));

        RaycastHit hitInfo;
        if (Physics.Raycast(player.transform.position + offset, direction, out hitInfo, followDistance))
        {
            transform.position = player.transform.position + offset + direction * hitInfo.distance * 0.7f;
        }
        else
        {
            transform.position = player.transform.position + offset + direction * followDistance;
        }
        transform.forward = direction.normalized * -1;
    }
}