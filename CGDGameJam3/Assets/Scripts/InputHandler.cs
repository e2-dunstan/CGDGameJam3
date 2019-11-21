using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    private static InputHandler _instance = null;
    [HideInInspector] public enum ActiveInput { MOUSE, CONTROLLER};

    ActiveInput activeInput = ActiveInput.MOUSE;

    private void Awake()
    {
        if (_instance == null) _instance = this;
    }

    public static InputHandler Instance()
    {
        if (_instance == null)
        {
            _instance = new InputHandler();
        }
        return _instance;
    }

    public float GetHorizontalInput(int playerNum)
    {
        return Input.GetAxisRaw("Horizontal" + playerNum);
    }

    public float GetVerticalInput(int playerNum)
    {
        return Input.GetAxisRaw("Vertical" + playerNum);
    }

    public float GetHorizontal2Input(int playerNum)
    {
        return Input.GetAxisRaw("RightStickX" + playerNum);
    }

    public float GetVertical2Input(int playerNum)
    {
        return Input.GetAxisRaw("RightStickY" + playerNum);
    }

    public bool GetSprintDown()
    {
        return GetKeyDown(KeyCode.LeftShift) || GetKeyDown(KeyCode.RightShift);
    }

    public bool GetSprintUp()
    {
        return GetKeyUp(KeyCode.LeftShift) || GetKeyUp(KeyCode.RightShift);
    }

    public bool GetSprintHold()
    {
        return GetKeyHold(KeyCode.LeftShift) || GetKeyHold(KeyCode.RightShift);
    }

    public bool GetKeyDown(KeyCode key)
    {
        return Input.GetKeyDown(key);
    }

    public bool GetKeyUp(KeyCode key)
    {
        return Input.GetKeyUp(key);
    }

    public bool GetKeyHold(KeyCode key)
    {
        return Input.GetKey(key);
    }

    public Vector3 GetMousePosVec3()
    {
        return Input.mousePosition;
    }

    public Vector2 GetMousePosVec2()
    {
        return new Vector2(Input.mousePosition.x, Input.mousePosition.y);
    }

    public bool GetLeftMouseButtonDown()
    {
        return Input.GetMouseButtonDown(0);
    }

    public bool GetRightMouseButtonDown()
    {
        return Input.GetMouseButtonDown(1);
    }

    public bool GetMiddleMouseButtonDown()
    {
        return Input.GetMouseButtonDown(2);
    }

    public bool GetLeftMouseButtonUp()
    {
        return Input.GetMouseButtonUp(0);
    }

    public bool GetRightMouseButtonUp()
    {
        return Input.GetMouseButtonUp(1);
    }

    public bool GetMiddleMouseButtonUp()
    {
        return Input.GetMouseButtonUp(2);
    }

    public bool GetLeftMouseButtonHold()
    {
        return Input.GetMouseButton(0);
    }

    public bool GetRightMouseButtonHold()
    {
        return Input.GetMouseButton(1);
    }

    public bool GetMiddleMouseButtonHold()
    {
        return Input.GetMouseButton(2);
    }

    public void EnableMouse(ActiveInput input)
    {
        activeInput = input;
        if(activeInput == ActiveInput.CONTROLLER)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public void MouseActive()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.Mouse1))
        {
            EnableMouse(ActiveInput.MOUSE);
        }
    }
    public ActiveInput GetActiveInput()
    {
        return activeInput;
    }
}
