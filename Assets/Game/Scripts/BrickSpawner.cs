using UnityEngine.Pool;
using UnityEngine;

public class BrickSpawner : MonoBehaviour
{
    [SerializeField] Brick _prefab;
    [SerializeField] AudioSource _audio;
    [SerializeField] ParticleSystem _particles;

    [Header("Settings")]
    [SerializeField] float _widthShift = 0.37f;
    [SerializeField] float _heightShift = 0.37f;
    [SerializeField] float _topRowY = 4f;

    [Header("Pool settings")]
    [SerializeField] int _defaultPoolCapacity = 20;
    [SerializeField] int _maxPoolSize = 50;

    private ObjectPool<Brick> _pool;

    private void Awake()
    {
        _pool = new ObjectPool<Brick>(
           createFunc: () => Instantiate(_prefab),
           actionOnGet: (brick) => brick.gameObject.SetActive(true),
           actionOnRelease: (brick) => brick.gameObject.SetActive(false),
           actionOnDestroy: (brick) => Destroy(brick.gameObject),
           collectionCheck: false,
           defaultCapacity: _defaultPoolCapacity, 
           maxSize: _maxPoolSize
       );
    }

    public void SpawnBricks(Level level)
    {
        Vector2 gridCenter = new Vector2(level.Columns * _widthShift / 2f, level.Rows * _heightShift / 2f);
        
        float yOffset = _topRowY - (-gridCenter.y + _heightShift / 2f);

        for (int row = 0; row < level.Rows; row++)
        {
            for (int col = 0; col < level.Columns; col++)
            {
                int hitPoints = level.Layout[row].Bricks[col];

                if (hitPoints == 0)
                    continue;

                Vector2 spawnPosition = new Vector2(col * _widthShift, -row * _heightShift + yOffset)
                    - gridCenter + new Vector2(_widthShift / 2f, -_heightShift / 2f);

                var brick = _pool.Get();
                brick.transform.position = spawnPosition;
                brick.Set(_pool, _audio, _particles, hitPoints);
            }
        }
    }
}

