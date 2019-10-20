using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    public GameObject target;
    // Start is called before the first frame update
    void OnTriggerEnter(Collider col)
    {
        EndOutlet endOutlet; 
        target.TryGetComponent<EndOutlet>(out endOutlet );
        if( endOutlet!= null)
            endOutlet.enabled = true;
    }
}
