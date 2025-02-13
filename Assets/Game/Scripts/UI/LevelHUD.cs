using UnityEngine;
using TMPro;

public class LevelHUD : MonoBehaviour
{
    [SerializeField] private TMP_Text _targets;
    [SerializeField] private TMP_Text _lives;
    [SerializeField] private TMP_Text _level;

    [SerializeField] private GameRules _gameManager;

    private void OnEnable()
    {
        BricksContainer.OnRemainingBricksChanged += RemainingBrickChanged;
        GameRules.OnLivesChanged += LivesChanged;
        GameRules.OnLevelLoaded += LevelLoaded;
    }

    private void OnDisable()
    {
        BricksContainer.OnRemainingBricksChanged -= RemainingBrickChanged;
        GameRules.OnLivesChanged -= LivesChanged;
        GameRules.OnLevelLoaded -= LevelLoaded;
    }

    private void Start()
    {
        LivesChanged(_gameManager.MaxLives);
    }

    private void LevelLoaded(int value)
    {
        _level.text = $"LEVEL: {value}";

        LivesChanged(_gameManager.MaxLives);
    }

    private void LivesChanged(int value)
    {
        _lives.text = $"LIVES: {value}";
    }

    private void RemainingBrickChanged(int value)
    {
        _targets.text = $"TARGETS: {value}";
    }
}
