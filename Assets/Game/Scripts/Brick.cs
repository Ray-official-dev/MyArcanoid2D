using System.Linq;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Brick : MonoBehaviour
{
    public BrickType InitialType => _initialType;

    [SerializeField] BrickData[] _data;

    private AudioSource _audio;
    private ParticleSystem _particles;
    private SpriteRenderer _renderer;

    private BrickType _initialType;
    private int _hitPoints;

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }

    public void Init(AudioSource audio, ParticleSystem particles, BrickType type)
    {
        _audio = audio;
        _particles = particles;
        _initialType = type;

        Setup(type);
    }

    private void Setup(BrickType type)
    {
        var data = _data.First((c) => type == c.Type ? true : false);
        
        _renderer.sprite = data.Sprite;
        _renderer.color = data.Color;
        _hitPoints = data.HitPoints;
    }

    public void TakeDamage(int value)
    {
        _hitPoints -= value;

        if (_hitPoints <= 0)
        {
            _audio.Play();

            _particles.transform.position = transform.position;
            var effect = _particles.main;
            effect.startColor = _renderer.color;
            _particles.Play();

            EventBus.Instance.BrickDestroyed(this);
            Destroy(gameObject);
            return;
        }

        Setup(_data.First((c) => _hitPoints == c.HitPoints ? true : false).Type);
    }

    public enum BrickType
    { 
        None,
        Simple,
        Reinforced,
        Durable
    }
}
