using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneTransitionManager : MonoBehaviour
{
    public static SceneTransitionManager instance;

    [SerializeField] private float transitionTime = 1.0f;

    private Image fadeImage;


    void Awake()
    {
        if (instance == null)
            instance = this;

        fadeImage = GetComponentInChildren<Image>();

        StartCoroutine(FadeIn());
    }


    public void ChangeScene(string scene)
    {
        StartCoroutine(FadeOut(scene));
    }

    private IEnumerator FadeIn()
    {
        Color colour = fadeImage.color;
        colour.a = 1;

        for(float t = 0.0001f; t < transitionTime; t+= Time.deltaTime)
        {
            colour.a = 1 - (t / transitionTime);
            fadeImage.color = colour;
            yield return null;
        }
    }

    private IEnumerator FadeOut(string scene = "")
    {
        Color colour = fadeImage.color;
        colour.a = 0;

        for (float t = 0.0001f; t < transitionTime; t += Time.deltaTime)
        {
            colour.a = t / transitionTime;
            fadeImage.color = colour;
            yield return null;
        }

        if (scene != "")
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(scene);
        }
    }
}
