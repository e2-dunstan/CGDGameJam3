using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    private static InputHandler _instance = null;

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

    public float GetHorizontalInput()
    {
        return Input.GetAxisRaw("Horizontal");
    }

    public float GetVerticalInput()
    {
        return Input.GetAxisRaw("Vertical");
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
}
