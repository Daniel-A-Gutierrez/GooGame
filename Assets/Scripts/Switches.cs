using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switches : MonoBehaviour
{
    [SerializeField] Transform thingToMove;
    [SerializeField] Vector3 moveOffset;

    [SerializeField] Vector3 switchRotation;
    bool activated;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!activated && other.tag == "Player")
        {
            activated = true;
            transform.Rotate(switchRotation);
            StartCoroutine(Activate());
        }
    }

    IEnumerator Activate()
    {
        for (int i = 0; i < 60; i++)
        {
            thingToMove.position += moveOffset / 60f;
            yield return null;
        }
    }
}
