using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{

    [SerializeField] Button[] menuObjects;
    bool joystickCentred = true;
    int playerCount = 1;
    // Start is called before the first frame update
    void Start()
    {
        menuObjects = FindObjectsOfType<Button>();
    }

    // Update is called once per frame
    void Update()
    {
        if (InputHandler.Instance().GetActiveInput() != InputHandler.ActiveInput.CONTROLLER)
        {
            playerCount = 1;
            for(int i = 0; i < menuObjects.Length; ++i)
            {
                var colour = menuObjects[i].colors;
                //colour.normalColor = new Color(1.0f, 1.0f, 1.0f);
                //menuObjects[i].colors = colour;

                //if(i > 0)
                //{
                //    menuObjects[i].interactable = false;
                //}
            }

            float j = InputHandler.Instance().GetHorizontalInput(1);
            float k = InputHandler.Instance().GetVerticalInput(1);
            float l = Input.GetAxis("JoystickA");
            if (j > 0.5f || k > 0.5f || l > 0.5f)
            {
                InputHandler.Instance().EnableMouse(InputHandler.ActiveInput.CONTROLLER);
            }
        }
        else
        {

            for (int i = 0; i < menuObjects.Length; ++i)
            {
                menuObjects[i].interactable = true;
                var colours = menuObjects[i].colors;
                colours.normalColor = new Color(1.0f, 1.0f, 1.0f);
                menuObjects[i].colors = colours;
                menuObjects[i].enabled = false;
            }

            var colour = menuObjects[playerCount - 1].colors;
            colour.normalColor = new Color(0.0f, 1.0f, 0.0f);
            menuObjects[playerCount - 1].colors = colour;

            if (InputHandler.Instance().GetVerticalInput(1) > 0.5f && joystickCentred)
            {
                joystickCentred = false;
                playerCount--;
                if (playerCount < 1) { playerCount = 2; }
            }
            else if (InputHandler.Instance().GetVerticalInput(1) < -0.5f && joystickCentred)
            {
                joystickCentred = false;
                playerCount++;
                if (playerCount > 2) { playerCount = 1; }
            }
            else if (InputHandler.Instance().GetVerticalInput(1) < 0.2f &&
                InputHandler.Instance().GetVerticalInput(1) > -0.2f)
            {
                joystickCentred = true;
            }
            else if (Input.GetAxis("JoystickA") > 0.5f)
            {
                LoadScene();
            }
        }

        PlayerManager.Instance().SetPlayerCount(playerCount);
        InputHandler.Instance().MouseActive();

    }

    public void LoadScene()
    {
        SceneManager.LoadScene("LewisScene");
    }

    public void SetDefaultPlayerCount()
    {
        PlayerManager.Instance().SetPlayerCount(0);
    }
}
