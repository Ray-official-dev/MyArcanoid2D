using UnityEngine;

[CreateAssetMenu(menuName = Constans.SOPath + "New GameRulesConfig")]
public class GameRulesConfig : ScriptableObject
{
    public Level[] Levels => _levels;
    public int MaxLives => _maxLives;
    public int StartLevel => _startLevel;

    [SerializeField] Level[] _levels;
    [SerializeField] int _maxLives;

    private int _startLevel;

    public void SetStartLevelIndex(int value)
    {
        _startLevel = value;
    }
}