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
        StartCoroutine(Transition(sceneName));
    }

    IEnumerator Transition(string sceneName)
    {
        for (int i = 0; i < 60; i++)
        {
            rectTransform.localPosition -= new Vector3(0, 18);
            rectTransform.sizeDelta += new Vector2(0, 36);
            yield return null;
        }

        rectTransform.RotateAround(rectTransform.parent.position, Vector3.forward, 180);

        for (int i = 0; i < 60; i++)
        {
            yield return null;
        }

        if (sceneName != "")
            yield return SceneManager.LoadSceneAsync(sceneName);

        for (int i = 0; i < 60; i++)
        {
            rectTransform.localPosition -= new Vector3(0, 18);
            rectTransform.sizeDelta -= new Vector2(0, 36);
            yield return null;
        }

        rectTransform.RotateAround(rectTransform.parent.position, Vector3.forward, 180);
    }
}
