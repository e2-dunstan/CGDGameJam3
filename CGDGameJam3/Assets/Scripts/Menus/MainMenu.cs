using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Transform options;
    [SerializeField] private string nextScene;
    private int currentSelectedOption = 0;

    private InputHandler inputHandler;

    [SerializeField] private Text title;
    private Color defaultTitleColour;
    private Vector3 defaultTitlePos;

    private float timeElapsed = 0;
    private float flashDelay = 1;

    private bool axisHeld = false;


    private void Start()
    {
        defaultTitleColour = title.color;
        defaultTitlePos = title.transform.position;

        inputHandler = InputHandler.Instance();
        UpdateOptions();
    }


    private void Update()
    {
        if (title != null)
        {
            if (timeElapsed >= flashDelay)
            {
                StartCoroutine(GlitchTitle());

                timeElapsed = 0;
                flashDelay = Random.Range(1f, 5f);
            }
            else timeElapsed += Time.deltaTime;
        }


        if (VerticalInputDetected()) UpdateOptions();

        if (inputHandler.GetKeyUp(KeyCode.Return) || inputHandler.GetKeyUp(KeyCode.Space))
        {
            if (currentSelectedOption == 0) SceneManager.LoadScene(nextScene);
            else if (currentSelectedOption == 1) Application.Quit();
        }
    }

    private void UpdateOptions()
    {
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

    private bool VerticalInputDetected()
    {
        if (axisHeld && (inputHandler.GetLeftStickY(1) > 0.5f || inputHandler.GetLeftStickY(1) < -0.5f)
            && inputHandler.GetLeftStickY(1) != 0)
        {
            return false;
        }
        axisHeld = false;
        if (inputHandler.GetLeftStickY(1) == 0) return false;

        if (inputHandler.GetLeftStickY(1) > 0)
        {
            axisHeld = true;
            currentSelectedOption++;
        }
        else if (inputHandler.GetLeftStickY(1) < 0)
        {
            axisHeld = true;
            currentSelectedOption--;
        }
        if (currentSelectedOption >= options.childCount - 1) currentSelectedOption = 0;
        else if (currentSelectedOption <= 0) currentSelectedOption = options.childCount - 1;

        return true;
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
