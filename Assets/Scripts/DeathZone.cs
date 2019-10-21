using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathZone : MonoBehaviour
{
    [SerializeField] CameraFollow cameraFollow;
    [SerializeField] MainPC mainPC;

    bool activated = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if (!activated)
        {
            activated = true;
            mainPC.enabled = false;
            cameraFollow.enabled = false;
            SceneTransition.instance.StartTransition(SceneManager.GetActiveScene().name, 30);
        }
    }
}
