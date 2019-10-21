using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public GameObject MainMenu;
    public GameObject Credits;
    public GameObject Instructions;
    public AudioManager audioManager;



    private void Start()
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        //audioManager.Play("funky chill loop 2");
        MainMenu.SetActive(true);
        //source = GetComponent<AudioSource>();
    }

    public void PlayGame()
    {
        SceneTransition.instance.StartTransition("Level 1");
    }

    public void EnterInstructions()
    {
        MainMenu.SetActive(false);
        Instructions.SetActive(true);
    }

    public void EnterCredits()
    {
        MainMenu.SetActive(false);
        Credits.SetActive(true);
    }

    public void ExitInstructions()
    {
        MainMenu.SetActive(true);
        Instructions.SetActive(false);
    }

    public void ExitCredits()
    {
        MainMenu.SetActive(true);
        Instructions.SetActive(false);
    }


    public void QuitGame()
    {
        Application.Quit();
    }
}
