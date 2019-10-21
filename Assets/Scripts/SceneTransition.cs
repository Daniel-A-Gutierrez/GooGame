using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    [SerializeField] RectTransform rectTransform = null;
    public static SceneTransition instance = null;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartTransition(string sceneName)
    {
        StartCoroutine(Transition(sceneName, 0));
    }

    public void StartTransition(string sceneName, int delay)
    {
        StartCoroutine(Transition(sceneName, delay));
    }

    IEnumerator Transition(string sceneName, int delay)
    {
        for(int i = 0; i < delay; i++)
        {
            yield return null;
        }

        for (int i = 29; i >= 0; i--)
        {
            rectTransform.localPosition = new Vector3(0, i * 48 - 108);
            yield return null;
        }

        for (int i = 0; i < 60; i++)
        {
            yield return null;
        }

        if (sceneName != "")
            yield return SceneManager.LoadSceneAsync(sceneName);

        for (int i = 0; i < 30; i++)
        {
            rectTransform.localPosition = new Vector3(0, i * -48 - 108);
            yield return null;
        }

        rectTransform.localPosition = new Vector3(0, 1600);
    }
}
