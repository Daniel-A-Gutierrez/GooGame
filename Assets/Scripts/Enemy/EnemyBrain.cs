using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyController))]
[RequireComponent(typeof(EnemySenses))]
public class EnemyBrain : MonoBehaviour
{
    /*
    What info do i care about ? 
    Enemy Present ? 
    Enemy Position
    
    States
    
    patrolling
        //cycle through an array of points.
    attacking
        //stop moving, attack at player position

    
     */
    [HideInInspector]
    EnemySenses senses;
    [HideInInspector]
    EnemyController controller;
    int targetIndex;
    public List<Vector3> patrolPoints;

    void Awake()
    {
        senses = GetComponent<EnemySenses>();
        controller = GetComponent<EnemyController>();
        targetIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(senses.PlayerPresent)
        {
            controller.Attack(senses.PlayerPosition);
        }        
        else
        {
            controller.targetPosition = patrolPoints[targetIndex];
            if(transform.position.x == patrolPoints[targetIndex].x && transform.position.z == patrolPoints[targetIndex].z)
                IncrementPoint();
        }
    }

    public void IncrementPoint()
    {
        if(targetIndex >= patrolPoints.Count-1)
                targetIndex = 0;
            else
                targetIndex++;
    }

}
