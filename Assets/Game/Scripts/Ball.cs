using UnityEngine.Events;
using UnityEngine;
using System;

[RequireComponent(typeof(Rigidbody2D))]
public class Ball : MonoBehaviour
{
    public event Action OnDestroyed;

    public UnityEvent OnCollisionEnter;

    [SerializeField] private float _speed = 1;
    [SerializeField] private float _distanceFromPlatform = 1;

    private Paddle _paddle => Paddle.Instance;

    private Rigidbody2D _body;
    private bool _isPushed;

    private void Awake()
    {
        _body = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        OnCollisionEnter?.Invoke();

        if (collision.gameObject.TryGetComponent(out Paddle paddle))
            ReflectFromPaddle(collision, paddle);

        if (collision.gameObject.TryGetComponent(out Brick brick))
            brick.TakeDamage(1);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnDestroyed?.Invoke();
        Destroy(gameObject);
    }

    private void Update()
    {
        if (!_isPushed)
            transform.position = new Vector3(_paddle.transform.position.x, _paddle.transform.position.y + _distanceFromPlatform);
    }

    private void ReflectFromPaddle(Collision2D collision, Paddle paddle)
    {
        ContactPoint2D contact = collision.GetContact(0);
        float offset = paddle.transform.position.x - contact.point.x;
        Vector2 direction = new Vector2(offset, 1).normalized;
        _body.linearVelocity = direction * _body.linearVelocity.magnitude;
    }

    public void PushUp()
    {
        _body.linearVelocityY = _speed;
        _isPushed = true;
    }
}
