using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GooEntrance : MonoBehaviour
{
    [SerializeField] Vector3 direction = Vector3.zero;
    [SerializeField] CameraFollow cameraFollower;

    MainPC mainPC;
    // Start is called before the first frame update
    private void Awake()
    {
        mainPC = GetComponent<MainPC>();
        mainPC.enabled = false;
        cameraFollower.enabled = false;
    }

    void Start()
    {
        StartCoroutine(Entrance());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Entrance()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;

        Vector3 dirNorm = direction.normalized;

        for (int i = 0; i < 30; i++)
        {
            yield return null;
        }

        Vector3 scale = Vector3.one * 0.1f + new Vector3(Mathf.Abs(dirNorm.x), Mathf.Abs(dirNorm.y), Mathf.Abs(dirNorm.z)) * 0.9f;
        transform.localScale = scale;

        for (int i = 0; i < 5; i++)
        {
            transform.position += dirNorm * 0.1f;
            yield return null;
        }

        for (int i = 0; i < 5; i++)
        {
            transform.position += dirNorm * 0.1f;
            transform.localScale = Vector3.Lerp(scale, Vector3.one, (i + 1) / 10f);
            yield return null;
        }

        for (int i = 5; i < 10; i++)
        {
            transform.localScale = Vector3.Lerp(scale, Vector3.one, (i + 1) / 10f);
            yield return null;
        }

        rb.isKinematic = false;

        for (int i = 0; i < 30; i++)
        {
            yield return null;
        }

        cameraFollower.enabled = true;
        mainPC.enabled = true;
    }
}
