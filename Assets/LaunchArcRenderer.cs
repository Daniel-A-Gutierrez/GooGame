using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//The arc should also stop when it quote-on-quote hits an object...
//rewrite this to work with a Vector3
//should display outside GameMode

[RequireComponent(typeof(MeshFilter))]

public class LaunchArcRenderer : MonoBehaviour {

   

    public GameObject player;

    Vector3 player_position;   
    public Vector3 velocity_vector; 

    int resolution; //resolution of the arrow

    float g; //force of gravity

    LineRenderer lr;

    Vector3 last_velocity;
    Vector3 last_position;

    public bool show_bar; 

    private void Awake() {
        
        lr = GetComponent<LineRenderer>();
        resolution = lr.positionCount;
        g = Mathf.Abs(Physics2D.gravity.y);
    }

    

    // Start is called before the first frame update
    void Update() {
        lr.enabled = show_bar;

        if ( (last_velocity != velocity_vector || last_position != player_position) && show_bar)
        {
            lr.SetPositions(CalculateArcArray(velocity_vector));
            last_velocity = velocity_vector;
            last_position = player_position;
        } 
    }


    //create an array of vector3 positions for arc
    Vector3[] CalculateArcArray(Vector3 vi) {
        bool hasCollided = false;
        int last_point = 0;

        Vector3[] arcArray = new Vector3[resolution + 1];

        player_position = player.transform.position;

        arcArray[0] = player_position; 
        arcArray[1] = arcArray[0] + (vi * 0.05f);

        vi += Vector3.down * g * .05f;

       

        for (int i = 2; i <= resolution; i++) {
            if (!hasCollided)
            {
                arcArray[i] = arcArray[i - 1] + vi * 0.05f;
                vi += Vector3.down * g * .05f;
            }
            
            else
            {
                arcArray[i] = arcArray[last_point];
            }

            if (Physics.Raycast(arcArray[i-1], arcArray[i] - arcArray[i-1], .75f) )
            {
                hasCollided = true;
                last_point = i;
            }
        } 
        
        

        return arcArray;
    }

        
    }
