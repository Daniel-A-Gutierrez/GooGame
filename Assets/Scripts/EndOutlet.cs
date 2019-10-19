using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndOutlet : MonoBehaviour
{
    [SerializeField] string nextScene;
    [SerializeField] Vector3 direction = Vector3.zero;

    bool activated = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator OnTriggerEnter(Collider other)
    {
        if (!activated && other.tag == "Player")
        {
            activated = true;
            other.attachedRigidbody.isKinematic = true;

            Vector3 start = other.transform.position;

            other.GetComponent<JigglePhysics>().Squish(3, direction);

            for (int i = 0; i < 60; i++)
            {
                other.transform.position = Vector3.Lerp(start, transform.position + direction, i / 60f);
                yield return null;
            }

            for (int i = 0; i < 30; i++)
            {
                yield return null;
            }

            other.GetComponent<JigglePhysics>().enabled = false;

            Vector3 dirNorm = direction.normalized;

            for (int i = 0; i < 10; i++)
            {
                other.transform.localScale = new Vector3(1 - i / 10f * (1 - Mathf.Abs(dirNorm.x)), 1 - i / 10f * (1 - Mathf.Abs(dirNorm.y)), 1 - i / 10f * (1 - Mathf.Abs(dirNorm.z)));
                yield return null;
            }

            for (int i = 0; i < 10; i++)
            {
                other.transform.position += dirNorm * -0.2f;
                yield return null;
            }

            for (int i = 0; i < 30; i++)
            {
                yield return null;
            }

            SceneTransition.instance.StartTransition(nextScene);
        }
    }
}
