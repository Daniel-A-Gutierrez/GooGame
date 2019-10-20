using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySenses : MonoBehaviour
{

    public int playerLayer;
    //have a collider which indicates if the player is in it. 
    //if a player is in it, raycast from us to the player. if its interrupted, dont notice. otherwise, do. 
    [HideInInspector]
    public bool PlayerPresent = false;
    [HideInInspector]
    public Vector3 PlayerPosition;
    
    // Start is called before the first frame update

    void OnTriggerStay(Collider c)
    {
        if(c.gameObject.layer == playerLayer)
        PlayerPresent = true;
        PlayerPosition = c.gameObject.transform.position;
    }

    void OnTriggerExit(Collider c)
    {
        PlayerPresent = false;
    }
}
