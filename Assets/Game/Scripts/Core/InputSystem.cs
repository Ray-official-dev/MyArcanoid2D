using UnityEngine.InputSystem;
using UnityEngine;
using System;

public class InputSystem : MonoBehaviour
{
    private Touchscreen _screen;
    private GameInput _input;
    private Vector2 _previusTouchPosition;

    public event Action OnTouchCanceled;

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

    public void Enable() => OnEnable();

    public void Disable() => OnDisable();

    private void TouchCanceled(InputAction.CallbackContext context)
    {
        if (!_isEnabled)
            return;

        OnTouchCanceled?.Invoke();
    }

    public Vector2 TouchPosition()
    {
        if (!_isEnabled)
            return _previusTouchPosition;

        if (!_screen.press.isPressed)
            return _previusTouchPosition;

        Vector2 touchPosition = _screen.position.ReadValue();
        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(new Vector2(touchPosition.x, touchPosition.y));

        _previusTouchPosition = worldPosition;
        return worldPosition;
    }
}