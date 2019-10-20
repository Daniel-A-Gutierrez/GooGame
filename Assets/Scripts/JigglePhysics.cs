using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JigglePhysics : MonoBehaviour
{
    [SerializeField] Transform[] thingsToSquish = new Transform[0];
    [SerializeField] float height = 0.5f;

    [SerializeField] float bounceForce = 0.02f;
    [SerializeField] float k = 0.01f;
    [SerializeField] float c = 0.1f;

    float force;
    float squish;
    float squishChange;

    public Vector3 direction;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Squish(float force, Vector3 direction)
    {
        this.force += force;
        this.direction = direction.normalized;
    }

    // Update is called once per frame
    void Update()
    {
        squish += squishChange;
        squishChange += force * bounceForce;
        force = 0;

        squishChange += -squish * k - squishChange * c;

        foreach (Transform t in thingsToSquish)
        {
            t.rotation = Quaternion.FromToRotation(Vector3.up, direction);
            t.localScale = new Vector3(1 / SquishFunction(squish), SquishFunction(squish), 1 / SquishFunction(squish));
            t.localPosition = direction.normalized * (SquishFunction(squish) * height - height);
        }
    }

    float SquishFunction(float x)
    {
        if (x > 0)
        {
            return 1 / (1 + squish);
        }
        else
        {
            return  2 - 1 / (1 - squish);
        }
    }
}
