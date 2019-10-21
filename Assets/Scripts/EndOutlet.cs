using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndOutlet : MonoBehaviour
{
    [SerializeField] string nextScene;
    [SerializeField] Vector3 direction = Vector3.zero;

    [SerializeField] CameraFollow cameraFollower;
    [SerializeField] MainPC mainPC;

    [SerializeField] Vector2 cameraDirection;

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
            AudioManager am = GameObject.Find("AudioManager").GetComponent<AudioManager>();
            am.Play("lq stage clear");
            mainPC.enabled = false;

            activated = true;
            other.attachedRigidbody.isKinematic = true;

            Vector3 start = other.transform.position;

            other.GetComponent<JigglePhysics>().Squish(3, direction);

            cameraFollower.lockCamera = true;
            float startH = cameraFollower.horizDir;
            float startV = cameraFollower.vertDir;

            float hdisp = cameraDirection.x - startH % 360;
            if (hdisp > 180)
                hdisp = 360 - hdisp;

            for (int i = 0; i < 60; i++)
            {
                cameraFollower.horizDir = startH + hdisp * (i + 1) / 60f;
                cameraFollower.vertDir = Mathf.Lerp(startV, cameraDirection.y, (i + 1) / 60f);
                other.transform.position = Vector3.Lerp(start, transform.position + direction, (i + 1) / 60f);
                yield return null;
            }

            cameraFollower.enabled = false;

            for (int i = 0; i < 30; i++)
            {
                yield return null;
            }

            other.GetComponent<JigglePhysics>().enabled = false;
            other.transform.rotation = Quaternion.identity;
            am.Play("succ");
            am.Play("electric6");

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
