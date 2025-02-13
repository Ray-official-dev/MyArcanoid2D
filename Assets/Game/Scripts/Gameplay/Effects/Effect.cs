using System;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Effect : MonoBehaviour
{
    public static event Action<Effect> OnAdd;

    public EffectType Type => _type;
    public float Duration => _duration;

    private EffectType _type;
    private float _duration;

    private void OnEnable()
    {
        GameRules.OnLevelLoaded += LevelLoaded;
    }

    private void OnDisable()
    {
        GameRules.OnLevelLoaded -= LevelLoaded;
    }

    private void LevelLoaded(int obj)
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.TryGetComponent(out Paddle paddle))
            return;

        OnAdd?.Invoke(this);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
    }

    public void Setup(EffectConfig config)
    {
        _type = config.Type;
        _duration = config.Duration;
        GetComponent<SpriteRenderer>().sprite = config.Sprite;
    }

    public enum EffectType
    { 
        Multiball,
        ExpandPaddle,
        ShrinkPaddle
    }
}
