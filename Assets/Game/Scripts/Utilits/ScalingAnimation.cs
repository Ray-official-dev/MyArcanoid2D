using UnityEngine;

public class ScalingAnimation : MonoBehaviour
{
    [SerializeField] float _speed = 2f;
    [SerializeField] float _scaleAmount = 0.2f;  

    private Vector3 _initialScale;

    private void Start()
    {
        _initialScale = transform.localScale;
    }

    private void Update()
    {
        float scaleFactor = 1 + Mathf.Sin(Time.time * _speed) * _scaleAmount;
        transform.localScale = _initialScale * scaleFactor;
    }
}
