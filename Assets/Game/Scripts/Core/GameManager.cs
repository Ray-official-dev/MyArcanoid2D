using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Links")]
    [SerializeField] private Ball _ball;
    [SerializeField] private Paddle _paddle;
    [SerializeField] private InputSystem _input;
    [SerializeField] private BrickSpawner _brickSpawner;

    [Header("Settings")]
    [SerializeField] private Level[] _levels;
    [SerializeField] private float _ballDistanceFromPlatform;

    private bool _isGameStarted;

    private void OnEnable()
    {
        _input.OnTouchCanceled += TouchCanceled;
    }

    private void OnDisable()
    {
        _input.OnTouchCanceled -= TouchCanceled;
    }

    private void Start()
    {
        _brickSpawner.SpawnBricks(_levels[0]);
    }

    private void TouchCanceled()
    {
        if (_isGameStarted)
            return;

        _isGameStarted = true;
        _ball.PushUp();
    }

    private void Update()
    {
        if (!_isGameStarted)
        {
            _ball.transform.position = new Vector3(_paddle.transform.position.x, _paddle.transform.position.y + _ballDistanceFromPlatform);
        }
    }
}
