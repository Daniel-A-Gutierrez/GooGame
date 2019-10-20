using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] MainPC mainPC;
    //public int lives; //for testing purposes

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
        if (mainPC.HP + i >= 0 || mainPC.HP + i <= 3)
        {
            mainPC.HP += i;
        }
        image.sprite = sprite[mainPC.HP];
    }


    void LiveCountUpdate()
    {
        if (mainPC.HP > 3)
        {
            mainPC.HP = 3;
        }

        else if (mainPC.HP < 0)
        {
            mainPC.HP = 0;
        }
        image.sprite = sprite[mainPC.HP];
    }
}
