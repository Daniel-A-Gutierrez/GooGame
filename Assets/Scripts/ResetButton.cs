using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ResetButton : MonoBehaviour
{
    public KeyCode resetButton;
    void Update()
    {
        if(Input.GetKeyDown(resetButton))
            UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }
}
