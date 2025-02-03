using System.Linq;
using UnityEngine;
using System;

[RequireComponent(typeof(SpriteRenderer))]
public class Brick : MonoBehaviour
{
    public event Action<Brick> OnDestroyed;

    [SerializeField] BrickData[] _data;

    private AudioSource _audio;
    private ParticleSystem _particles;

    private int _hitPoints;

    private SpriteRenderer _renderer;

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }

    public void Init(AudioSource audio, ParticleSystem particles, int hitPoints)
    {
        _audio = audio;
        _particles = particles;
        _hitPoints = hitPoints;

        Setup(hitPoints);
    }

    private void Setup(int hitPoints)
    {
        var data = _data.First((brick) => brick.HitPoints == hitPoints ? true : false);

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
            _particles.startColor = _renderer.color;
            _particles.Play();

            OnDestroyed?.Invoke(this);
            Destroy(gameObject);
            return;
        }

        Setup(_hitPoints);
    }
}