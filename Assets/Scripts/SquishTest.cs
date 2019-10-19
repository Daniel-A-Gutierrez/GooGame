using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquishTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey)
        {
            GetComponent<Rigidbody>().velocity = new Vector3(Input.GetAxis("Horizontal") * 3, 3, Input.GetAxis("Vertical") * 3);
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        GetComponent<JigglePhysics>().Squish(collision.impulse.magnitude, collision.impulse);
    }
}
