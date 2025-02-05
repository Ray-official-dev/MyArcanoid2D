using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Effect : MonoBehaviour
{
    public EffectType Type => _type;
    public float Duration => _duration;

    private EffectType _type;
    private float _duration;

    private void OnEnable()
    {
        EventBus.Instance.OnLevelRestarted += LevelRestarted;
    }

    private void OnDisable()
    {
        EventBus.Instance.OnLevelRestarted -= LevelRestarted;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.TryGetComponent(out Paddle paddle))
            return;

        EventBus.Instance.EffectAdded(this);
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

    private void LevelRestarted()
    {
        Destroy(gameObject);
    }

    public enum EffectType
    { 
        Multiball,
        ExpandPaddle,
        ShrinkPaddle
    }
}
