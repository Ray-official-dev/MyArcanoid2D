using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Paddle : MonoSingleton<Paddle>
{
    [SerializeField] private InputSystem _input;

    private Rigidbody2D _body;
    private Vector2 _startPosition;

    protected override void Awake()
    {
        base.Awake();

        _body = GetComponent<Rigidbody2D>();
        _startPosition = transform.position;
    }

    private void FixedUpdate()
    {
        Vector2 touch = _input.TouchPosition();
        _body.MovePosition(new Vector2(touch.x, _startPosition.y));
    }
}
