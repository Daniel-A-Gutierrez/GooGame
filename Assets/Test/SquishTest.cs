using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquishTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody>().velocity = new Vector3(-5, 5);
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
        if (collision.impulse.sqrMagnitude > 0)
        {
            GetComponent<JigglePhysics>().Squish(collision.impulse.magnitude, collision.impulse);
        }
    }
}
