using UnityEngine;
using TMPro;

public class LevelHUD : MonoBehaviour
{
    [SerializeField] private TMP_Text _targets;
    [SerializeField] private TMP_Text _lives;
    [SerializeField] private TMP_Text _level;

    [SerializeField] private BricksContainer _spawner;
    [SerializeField] private GameManager _gameManager;

    private void OnEnable()
    {
        _spawner.OnRemainingBricksChanged += RemainingBrickChanged;
        _gameManager.OnLivesChanged += LivesChanged;
        _gameManager.OnLevelLoaded += LevelLoaded;
    }

    private void OnDisable()
    {
        _spawner.OnRemainingBricksChanged -= RemainingBrickChanged;
        _gameManager.OnLivesChanged -= LivesChanged;
        _gameManager.OnLevelLoaded -= LevelLoaded;
    }

    private void Start()
    {
        LivesChanged(_gameManager.MaxLives);
    }

    private void LevelLoaded(int value)
    {
        _level.text = $"Level: {value + 1}";

        LivesChanged(_gameManager.MaxLives);
    }

    private void LivesChanged(int value)
    {
        _lives.text = $"Lives: {value}";
    }

    private void RemainingBrickChanged(int value)
    {
        _targets.text = $"Targets: {value}";
    }
}
