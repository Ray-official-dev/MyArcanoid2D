using UnityEngine;
using TMPro;

public class NextLevelScreen : MonoBehaviour
{
    [SerializeField] Animator _animator;
    [SerializeField] string _triggerName;
    [SerializeField] TMP_Text _level;
    [SerializeField] GameObject _panel;

    private void OnEnable()
    {
        GameRules.OnLevelLoaded += LevelLoaded;    
    }

    private void OnDisable()
    {
        GameRules.OnLevelLoaded -= LevelLoaded;
    }

    private void LevelLoaded(int level)
    {
        _animator.SetTrigger(_triggerName);
        _level.text = $"Level: {level}";
    }
}
