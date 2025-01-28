using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Ball : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Paddle>(out _))
        {
            ContactPoint2D contact = collision.GetContact(0);

            float offset = transform.position.x - contact.point.x;
            Vector2 direction = new Vector2(offset, 1).normalized;

        }
    }
}
