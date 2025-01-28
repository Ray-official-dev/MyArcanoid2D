using UnityEngine;
using UnityEngine.InputSystem;
using System;

[RequireComponent(typeof(Rigidbody2D))]
public class Paddle : MonoBehaviour
{
    private Touchscreen _screen;
    private Camera _mainCamera;
    private Rigidbody2D _body;
    private Vector2 _startPosition;

    private void Awake()
    {
        _screen = Touchscreen.current;
        _mainCamera = Camera.main;
        _body = GetComponent<Rigidbody2D>();
        _startPosition = transform.position;

        if (_screen is null)
            throw new InvalidOperationException();
    }

    private void FixedUpdate()
    {
        if (_screen.press.isPressed)
        {
            Vector2 touchPosition = _screen.position.ReadValue();
            Vector2 worldPosition = _mainCamera.ScreenToWorldPoint(new Vector2(touchPosition.x, touchPosition.y));

            _body.MovePosition(new Vector2(worldPosition.x, _startPosition.y));
        }
    }
}
