using UnityEngine.Pool;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Brick : MonoBehaviour
{
    [SerializeField] BrickData[] _data;

    private AudioSource _audio;
    private ParticleSystem _particles;
    private ObjectPool<Brick> _pool;

    private int _hitPoints;

    private SpriteRenderer _renderer;

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
        gameObject.hideFlags = HideFlags.HideInHierarchy;
    }

    public void Set(ObjectPool<Brick> pool, AudioSource audio, ParticleSystem particles, int hitPoints)
    {
        _audio = audio;
        _particles = particles;
        _pool = pool;
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

            _pool.Release(this);
            return;
        }

        Setup(_hitPoints);
    }
}