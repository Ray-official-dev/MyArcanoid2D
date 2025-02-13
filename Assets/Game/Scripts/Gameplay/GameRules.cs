using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class GameRules : MonoBehaviour
{
    public static event Action<int> OnLevelLoaded;
    public static event Action<int> OnLivesChanged;

    public int CurrentLevel => _levelIndex;
    public int MaxLives => _config.MaxLives;
    public int LevelCount => _config.Levels.Length + 1;

    [Header("Links")]
    [SerializeField] BricksContainer _bricks;
    [SerializeField] BallsContainer _balls;
    [SerializeField] GameRulesConfig _config;

    private int _lives;
    private int _levelIndex;

    private bool _isGameStarted;

    private void OnEnable()
    {
        MobileInputSystem.OnTouchCanceled += TouchCanceled;
        BricksContainer.OnBricksFinished += BricksFinished;
        BallsContainer.OnAllBallsDestroyed += BallsFinished;
    }

    private void OnDisable()
    {
        MobileInputSystem.OnTouchCanceled -= TouchCanceled;
        BricksContainer.OnBricksFinished -= BricksFinished;
        BallsContainer.OnAllBallsDestroyed -= BallsFinished;

        Time.timeScale = 1;
    }

    private void Start()
    {
        _levelIndex = _config.StartLevel;
        LoadLevel();
    }

    public void Pause()
    {
        Time.timeScale = 0;
    }

    public void Resume()
    {
        Time.timeScale = 1;
    }

    private void BallsFinished()
    {
        _lives--;

        if (_lives == 0)
        {
            LoadLevel();
            return;
        }
        
        OnLivesChanged?.Invoke(_lives);

        _isGameStarted = false;
        _balls.SpawnBall();
    }

    private void LoadLevel()
    {
        _isGameStarted = false;
        _lives = _config.MaxLives;

        _balls.Clear();
        _bricks.Clear();
        _balls.SpawnBall();
        _bricks.SpawnBricks(_config.Levels[_levelIndex]);

        OnLevelLoaded?.Invoke(_levelIndex + 1);
    }

    private void BricksFinished()
    {
        if (Storage.GetLastSavedLevel() < _levelIndex)
            Storage.SaveLastLevel(_levelIndex);

        _levelIndex++;

        if (_levelIndex > _config.Levels.Length - 1)
        {
            SceneManager.LoadScene(0);
            return;
        }

        LoadLevel();
    }

    private void TouchCanceled()
    {
        if (_isGameStarted)
            return;

        _isGameStarted = true;
        _balls.PushBall();
    }
}
