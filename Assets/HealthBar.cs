using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    public int lives; //for testing purposes

    GameObject canvas;
    Image image;

    public Sprite[] sprite = new Sprite[4];




    // Start is called before the first frame update
    void Start()
    {

        image = GetComponent<Image>();
        image.sprite = sprite[0];
    }

    // Update is called once per frame
    void Update()
    {
        LiveCountUpdate();
    }

    void LiveCountSetter(int i)
    {
        if (lives + i >= 0 || lives + i <= 3)
        {
            lives += i;
        }
        image.sprite = sprite[lives];
    }


    void LiveCountUpdate()
    {
        if (lives > 3)
        {
            lives = 3;
        }

        else if (lives < 0)
        {
            lives = 0;
        }
        image.sprite = sprite[lives];
    }
}
