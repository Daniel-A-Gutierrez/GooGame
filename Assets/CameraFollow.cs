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
    //actually just gonna do something else.

    /*
        predef distance from player, camera transform. 
        rotate by mouse movement
        in player controller interpret forward as camera.forward
    */
    
    public void Start()
    {
        oldPlayerPosition = player.transform.position;
        transform.forward = player.transform.position + offset - transform.position;
        transform.position = player.transform.position + offset -player.transform.forward*followDistance;
    }
    void LateUpdate () 
    {
        transform.Translate(player.transform.position-oldPlayerPosition , Space.World);
        oldPlayerPosition = player.transform.position;

        float dy = - VerticalSensitivity * Input.GetAxis("Mouse Y");
        float dh = HorizontalSensitivity * Input.GetAxis("Mouse X");
        transform.RotateAround(player.transform.position,Vector3.up,dh);

        //clamp the y rotation
        bool lookingUp = dy<0;
        bool lookingDown = dy>0;
        if( !((lookingUp & (transform.eulerAngles.x+90)%360 < 10 -dy ) | (lookingDown & (transform.eulerAngles.x+90)%360 >170-dy)))
            transform.RotateAround(player.transform.position,transform.right,dy);

        transform.forward = (player.transform.position+offset-transform.position).normalized;

    }
}