using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Transform options;
    private int currentSelectedOption = 0;

    [SerializeField] private Text title;
    private Color defaultTitleColour;
    private Vector3 defaultTitlePos;

    private float timeElapsed = 0;
    private float flashDelay = 1;

    private bool disableInput = false;
    private float timeSinceLastInput = 0;

    private void Start()
    {
        defaultTitleColour = title.color;
        defaultTitlePos = title.transform.position;
    }


    private void Update()
    {
        if (timeElapsed >= flashDelay)
        {
            StartCoroutine(GlitchTitle());

            timeElapsed = 0;
            flashDelay = Random.Range(1f, 5f);
        }
        else timeElapsed += Time.deltaTime;

        //GetInputs();

        for (int i = 0; i < options.childCount; i++)
        {
            if (i == currentSelectedOption)
            {
                options.GetChild(i).GetComponentInChildren<Text>().color = Color.red;
            }
            else
            {
                options.GetChild(i).GetComponentInChildren<Text>().color = Color.white;
            }
        }
    }

    private IEnumerator GlitchTitle()
    {
        int flashCount = Random.Range(1, 4);
        for (int i = 0; i < flashCount; i++)
        {
            title.color = Color.white;
            title.transform.position += new Vector3(Random.Range(-50, 50), Random.Range(-50, 50), 0);

            yield return new WaitForSeconds(Random.Range(0.08f, 0.15f));

            title.color = defaultTitleColour;
            title.transform.position = defaultTitlePos;

            yield return new WaitForSeconds(Random.Range(0.08f, 0.15f));
        }
    }
}
