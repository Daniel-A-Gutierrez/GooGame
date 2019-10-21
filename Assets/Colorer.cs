using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colorer : MonoBehaviour
{
    public Color color;
    // Start is called before the first frame update
    [ExecuteInEditMode]
    void Start()
    {
        GetComponent<Renderer>().material.SetColor("_Color", color);
        //cube.GetComponent<Material>().EnableKeyword("_Color");//might need to do this or something
    }
}
