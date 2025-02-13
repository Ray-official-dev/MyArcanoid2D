using UnityEngine.InputSystem;
using UnityEngine;
using System;

public class MobileInputSystem : MonoBehaviour
{
    public static event Action OnTouchCanceled;

    private Touchscreen _screen;
    private GameInput _input;
    private Vector2 _previusTouchPosition;

    public bool _isEnabled { get; private set; }

    private void Awake()
    {
        _input = new GameInput();
        _screen = Touchscreen.current;
    }

    private void OnEnable()
    {
        _input.Game.Touch.canceled += TouchCanceled;

        _input.Enable();
        _isEnabled = true;
    }

    private void OnDisable()
    {
        _input.Game.Touch.canceled -= TouchCanceled;

        _input.Disable();
        _isEnabled = false;
    }

    private void TouchCanceled(InputAction.CallbackContext context)
    {
        if (!_isEnabled)
            return;

        OnTouchCanceled?.Invoke();
    }

    public Vector2 GetTouchPosition()
    {
        if (!_isEnabled)
            return _previusTouchPosition;

        Vector2 touchPosition = _screen.position.ReadValue();
        Vector2 worldTouchPosition = Camera.main.ScreenToWorldPoint(new Vector2(touchPosition.x, touchPosition.y));

        _previusTouchPosition = worldTouchPosition;
        return worldTouchPosition;
    }
}
