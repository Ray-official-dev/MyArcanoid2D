using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelIconsSpawner : MonoBehaviour
{
    [SerializeField] private LevelIcon _prefab;
    [SerializeField] private Transform _content;
    [SerializeField] private GameRulesConfig _gameConfig;

    private void Start()
    {
        int lastSavedLevel = Storage.GetLastSavedLevel();

        for (int index = 0; index < _gameConfig.Levels.Length; index++)
        {
            bool isLocked = default;

            if (index > lastSavedLevel)
                isLocked = true;

            var icon = Instantiate(_prefab, _content);
            icon.Init(index, isLocked);

            icon.OnClicked += (level) => 
            {
                if (level > Storage.GetLastSavedLevel())
                    return;

                _gameConfig.SetStartLevelIndex(level);
                SceneManager.LoadScene(Constans.GameScene);
            };
        }
    }
}
