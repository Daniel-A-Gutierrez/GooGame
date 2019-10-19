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

    Vector3 direction;

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
            t.localScale = new Vector3(SquishFunction(squish, direction.x), SquishFunction(squish, direction.y), SquishFunction(squish, direction.z));
            t.localPosition = new Vector3((SquishFunction(squish, direction.x) * height - height) * direction.x,
                (SquishFunction(squish, direction.y) * height - height) * direction.y, (SquishFunction(squish, direction.z) * height - height) * direction.z);
        }
    }

    float SquishFunction(float x, float dir)
    {
        float y;
        if (x > 0)
        {
            y =  1 / (1 + squish);
        }
        else
        {
            y =  2 - 1 / (1 - squish);
        }

        return Mathf.Lerp(1 / y, y, Mathf.Abs(dir));
    }
}
