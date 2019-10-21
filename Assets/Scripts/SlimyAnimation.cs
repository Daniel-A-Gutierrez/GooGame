using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimyAnimation : MonoBehaviour
{
    Material material;
    [SerializeField] float flowSpeed = 0.005f;

    // Start is called before the first frame update
    void Start()
    {
        material = GetComponent<MeshRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        material.mainTextureOffset += new Vector2(0, flowSpeed);
    }
}
