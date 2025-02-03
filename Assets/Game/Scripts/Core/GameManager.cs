using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public event Action<int> OnLevelLoaded;
    public event Action<int> OnLivesChanged;

    public int CurrentLevel => _currentLevel;
    public int MaxLives => _maxLives;
    
    [Header("Links")]
    [SerializeField] Ball _ballPrefab;

    [Header("Settings")]
    [SerializeField] Level[] _levels;
    [SerializeField] int _maxLives;

    [SerializeField] InputSystem _input;
    [SerializeField] BricksContainer _container;

    private int _currentLives;
    private int _currentLevel;

    private bool _isGameStarted;
    private Ball _ball;

    private void OnEnable()
    {
        _input.OnTouchCanceled += TouchCanceled;
        _container.OnBricksFinished += BricksFinished;
    }

    private void OnDisable()
    {
        _input.OnTouchCanceled -= TouchCanceled;
        _container.OnBricksFinished -= BricksFinished;
    }

    private void Start()
    {
        LoadLevel();
    }

    private void BallDestroyed()
    {
        _currentLives--;

        if (_currentLives == 0)
        {
            LoadLevel();
            return;
        }
        
        OnLivesChanged?.Invoke(_currentLives);

        _isGameStarted = false;
        SpawnBall();
    }

    private void LoadLevel()
    {
        _isGameStarted = false;
        _currentLives = _maxLives;

        if (_ball is not null)
            Destroy(_ball.gameObject);

        _container.ClearAllBricks();
        SpawnBricks();
        SpawnBall();

        OnLevelLoaded?.Invoke(_currentLevel);
    }

    private void SpawnBall()
    {
        _ball = Instantiate(_ballPrefab);
        _ball.OnDestroyed += BallDestroyed;
    }

    private void BricksFinished()
    {
        _currentLevel++;
        OnLevelLoaded?.Invoke(_currentLevel);
        LoadLevel();
    }

    private void SpawnBricks()
    {
        _container.SpawnBricks(_levels[_currentLevel]);
    }

    private void TouchCanceled()
    {
        if (_isGameStarted)
            return;

        _isGameStarted = true;
        _ball.PushUp();
    }
}
