using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieEffect : MonoBehaviour
{
    [SerializeField] GameObject sphere;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Die(Vector3 position)
    {
        for (int i = 0; i < 60; i++)
        {
            StartCoroutine(Effect(position));
        }
    }

    IEnumerator Effect(Vector3 position)
    {
        GameObject obj = Instantiate(sphere, transform);
        obj.SetActive(true);
        obj.transform.position = position;
        float scale = Random.Range(0.1f, 0.5f);
        Vector3 dir = Random.insideUnitSphere;

        for (int i = 59; i >= 0; i--)
        {
            obj.transform.position += dir * 0.1f;
            dir += new Vector3(0, -0.1f);
            obj.transform.localScale = Vector3.one * scale * i / 60f;
            yield return null;
        }
    }
}
