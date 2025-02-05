using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public event Action<int> OnLevelLoaded;
    public event Action<int> OnLivesChanged;

    public int CurrentLevel => _currentLevel;
    public int MaxLives => _maxLives;

    [Header("Settings")]
    [SerializeField] Level[] _levels;
    [SerializeField] int _maxLives;

    [SerializeField] InputSystem _input;
    [SerializeField] BricksContainer _bricks;
    [SerializeField] BallsContainer _balls;

    private int _currentLives;
    private int _currentLevel;

    private bool _isGameStarted;

    private void OnEnable()
    {
        _input.OnTouchCanceled += TouchCanceled;
        _bricks.OnBricksFinished += BricksFinished;
        _balls.OnAllBallsDestroyed += AllBallsDestroyed;
    }

    private void OnDisable()
    {
        _input.OnTouchCanceled -= TouchCanceled;
        _bricks.OnBricksFinished -= BricksFinished;
        _balls.OnAllBallsDestroyed -= AllBallsDestroyed;
    }

    private void Start()
    {
        LoadLevel();
    }

    private void AllBallsDestroyed()
    {
        _currentLives--;

        if (_currentLives == 0)
        {
            LoadLevel();
            return;
        }
        
        OnLivesChanged?.Invoke(_currentLives);

        _isGameStarted = false;
        _balls.SpawnBall();
    }

    private void LoadLevel()
    {
        _isGameStarted = false;
        _currentLives = _maxLives;

        _balls.Clear();
        _bricks.Clear();
        _balls.SpawnBall();
        SpawnBricks();

        OnLevelLoaded?.Invoke(_currentLevel);

        EventBus.Instance.LevelRestarted();
    }

    private void BricksFinished()
    {
        _currentLevel++;
        OnLevelLoaded?.Invoke(_currentLevel);
        LoadLevel();
    }

    private void SpawnBricks()
    {
        _bricks.SpawnBricks(_levels[_currentLevel]);
    }

    private void TouchCanceled()
    {
        if (_isGameStarted)
            return;

        _isGameStarted = true;
        _balls.PushBall();
    }
}
