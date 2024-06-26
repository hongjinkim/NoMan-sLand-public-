using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class InputManager :MonoBehaviour
{
    private PlayerInput playerInput;

    public delegate void StartTouchEvent0(Vector2 position, float time);
    public event StartTouchEvent0 OnStartTouch0;
    public event StartTouchEvent0 OnStartTouch1;

    public delegate void EndTouchEvent0(Vector2 postion, float time);
    public event EndTouchEvent0 OnEndTouch0;
    public event EndTouchEvent0 OnEndTouch1;

    Transform root;
    public void Init()
    {
        root = GetComponent<Transform>();
        if (root == null)
        {
            root = new GameObject { name = "@InputManager" }.transform;
        }
        Object.DontDestroyOnLoad(root);
        LinkInput();
    }

    private void LinkInput()
    {
        playerInput = new PlayerInput();
        playerInput.Touch.TouchPress0.started += context => StartTouch0(context);
        playerInput.Touch.TouchPress0.canceled += context => EndTouch0(context);

        playerInput.Touch.TouchPress1.started += context => StartTouch1(context);
        playerInput.Touch.TouchPress1.canceled += context => EndTouch1(context);
    }

    private void StartTouch0(InputAction.CallbackContext context)
    {
        if (OnStartTouch0 == null) return;
        OnStartTouch0(playerInput.Touch.TouchPosition0.ReadValue<Vector2>(), (float)context.startTime);
    }
    private void EndTouch0(InputAction.CallbackContext context)
    {
        if (OnEndTouch0 == null) return;
        OnEndTouch0(playerInput.Touch.TouchPosition0.ReadValue<Vector2>(), (float)context.time);
    }
    private void StartTouch1(InputAction.CallbackContext context)
    {
        if (OnStartTouch1 == null) return;
        OnStartTouch1(playerInput.Touch.TouchPosition1.ReadValue<Vector2>(), (float)context.startTime);
    }
    private void EndTouch1(InputAction.CallbackContext context)
    {
        if (OnEndTouch1 == null) return;
        OnEndTouch1(playerInput.Touch.TouchPosition1.ReadValue<Vector2>(), (float)context.time);
    }
}
