using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Ball : MonoBehaviour
{
    [SerializeField] private float _speed = 1;

    private Rigidbody2D _body;

    private void Awake()
    {
        _body = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Paddle paddle))
        {
            ContactPoint2D contact = collision.GetContact(0);
            float offset = paddle.transform.position.x - contact.point.x;
            Vector2 direction = new Vector2(offset, 1).normalized;
            _body.linearVelocity = direction * _body.linearVelocity.magnitude;
        }
    }

    public void PushUp()
    {
        _body.AddForce(Vector2.up * (_speed * 100));
    }
}
