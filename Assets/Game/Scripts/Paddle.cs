using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider2D))]
public class Paddle : MonoSingleton<Paddle>
{
    [SerializeField] private InputSystem _input;
    [SerializeField] private float _expandSize;
    [SerializeField] private float _shrinkSize;
    [SerializeField] private Vector2 _defaultSize;

    private Rigidbody2D _body;
    private Vector2 _startPosition;
    private SpriteRenderer _spriteRenderer;
    private BoxCollider2D _collider;   

    protected override void Awake()
    {
        base.Awake();

        _body = GetComponent<Rigidbody2D>();
        _startPosition = transform.position;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _collider = GetComponent<BoxCollider2D>();
    }

    private void OnEnable()
    {
        EventBus.Instance.OnEffectAdded += EffectAdded;
    }
    private void OnDisable()
    {
        EventBus.Instance.OnEffectAdded -= EffectAdded;
    }

    private void EffectAdded(Effect effect)
    {
        if (effect.Type == Effect.EffectType.ExpandPaddle)
            AddEffect(_expandSize, effect.Duration);

        if (effect.Type == Effect.EffectType.ShrinkPaddle)
            AddEffect(_shrinkSize, effect.Duration);
    }

    private void FixedUpdate()
    {
        Vector2 touch = _input.TouchPosition();
        _body.MovePosition(new Vector2(touch.x, _startPosition.y));
    }

    private void AddEffect(float size, float duration)
    {
        SetupSize(size);
        StopAllCoroutines();
        StartCoroutine(SetupSizeAfterTime(duration, _defaultSize.x));
    }

    private void SetupSize(float size)
    {
        _spriteRenderer.size = new Vector2(size, _spriteRenderer.size.y);
        _collider.size = new Vector2(size, _collider.size.y);
    }

    private IEnumerator SetupSizeAfterTime(float duration, float size)
    {
        yield return new WaitForSeconds(duration);
        SetupSize(size);
    }

    private void OnValidate()
    {
        if (Application.isPlaying)
            return;

        GetComponent<BoxCollider2D>().size = _defaultSize;
        GetComponent<SpriteRenderer>().size = _defaultSize;
    }

    private void Reset()
    {
        _expandSize = 1.5f;
        _shrinkSize = 0.5f;
        _defaultSize = Vector2.one;

        OnValidate();
    }
}
