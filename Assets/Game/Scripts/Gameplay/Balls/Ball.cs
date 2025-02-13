using UnityEngine.Events;
using UnityEngine;
using System;

[RequireComponent(typeof(Rigidbody2D))]
public class Ball : MonoBehaviour
{
    public event Action<Ball> OnDestroying;

    public UnityEvent OnCollisionEnter;

    [SerializeField] private float _speed = 1;
    [SerializeField] private float _distanceFromPlatform = 1;

    private Paddle _paddle => Paddle.Instance;

    private Rigidbody2D _body;
    private bool _isPushed;
    private Vector2 _lastVelocity;

    private void Awake()
    {
        _body = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        OnCollisionEnter?.Invoke();

        if (collision.gameObject.TryGetComponent(out Paddle paddle))
            ReflectFromPaddle(collision, paddle);
        else
            ReflectFromWall(collision);

        if (collision.gameObject.TryGetComponent(out Brick brick))
            brick.TakeDamage(1);
    }

    private void OnTriggerEnter2D(Collider2D collision) //штрафная зона (под платформой)
    {
        OnDestroying?.Invoke(this);
        Destroy(gameObject);
    }

    private void FixedUpdate()
    {
        if (!_isPushed)
            return;

        if (_body.linearVelocity.magnitude < _speed)
        {
            var direction = _body.linearVelocity.normalized;
            _lastVelocity = direction * _speed;
        }

        if (_body.linearVelocity.magnitude < 0.1f)
            Push(Vector2.down);

        _lastVelocity = _body.linearVelocity;
    }

    private void Update()
    {
        if (!_isPushed)
            transform.position = new Vector3(_paddle.transform.position.x, _paddle.transform.position.y + _distanceFromPlatform);
    }

    private void ReflectFromWall(Collision2D collision)
    {
        var normal = collision.GetContact(0).normal;
        var reflectVelocity = Vector2.Reflect(_lastVelocity, normal);
        _body.linearVelocity = reflectVelocity.normalized * _speed;
    }

    private void ReflectFromPaddle(Collision2D collision, Paddle paddle)
    {
        ContactPoint2D contact = collision.GetContact(0);
        float offset = paddle.transform.position.x - contact.point.x;
        Vector2 direction = new Vector2(offset, 1).normalized;
        _body.linearVelocity = direction * _speed;
    }

    public void Push(Vector2 direction)
    {
        _body.linearVelocity = direction * _speed;
        _isPushed = true;
    }
}
