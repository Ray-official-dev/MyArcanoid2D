using UnityEngine;

public class ShakeEffect : MonoBehaviour
{
    [SerializeField] float _strength = 0.1f;
    [SerializeField] float _duration = 0.5f;

    private Vector3 _originalPosition;
    private float _shakeTimer;
    private bool _isShaking = false;

    private void Start()
    {
        _originalPosition = transform.localPosition;
    }

    private void Update()
    {
        if (_isShaking)
        {
            if (_shakeTimer > 0)
            {
                transform.localPosition = _originalPosition + Random.insideUnitSphere * _strength;
                _shakeTimer -= Time.deltaTime;
            }
            else
            {
                _isShaking = false;
                _shakeTimer = 0f;
                transform.localPosition = _originalPosition;
            }
        }
    }

    public void Shake()
    {
        _shakeTimer = _duration;
        _isShaking = true;
    }
}
