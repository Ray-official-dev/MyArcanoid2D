using System.Collections.Generic;
using UnityEngine;
using System;

public class BricksContainer : MonoBehaviour
{
    public static event Action<int> OnRemainingBricksChanged;
    public static event Action OnBricksFinished;

    [SerializeField] Brick _prefab;
    [SerializeField] AudioSource _audio;
    [SerializeField] ParticleSystem _particles;
    [SerializeField] ShakeEffect _camera;

    [Header("Settings")]
    [SerializeField] float _widthShift = 0.37f;
    [SerializeField] float _heightShift = 0.37f;
    [SerializeField] float _topRowY = 4f;

    private List<Brick> _remainingBricks;

    private void Awake()
    {
        _remainingBricks = new List<Brick>();
    }

    private void OnEnable()
    {
        Brick.OnDestroying += BrickDestroyed;
    }

    private void OnDisable()
    {
        Brick.OnDestroying -= BrickDestroyed;
    }

    public void SpawnBricks(Level level)
    {
        Vector2 gridCenter = new Vector2(level.Columns * _widthShift / 2f, level.Rows * _heightShift / 2f);
        
        float yOffset = _topRowY - (-gridCenter.y + _heightShift / 2f);

        int orderInLayer = default;

        for (int row = 0; row < level.Rows; row++)
        {
            for (int col = 0; col < level.Columns; col++)
            {
                orderInLayer++;
                Brick.BrickType type = level.Layout[row].Bricks[col];

                if (type == Brick.BrickType.None)
                    continue;

                Vector2 spawnPosition = new Vector2(col * _widthShift, -row * _heightShift + yOffset)
                    - gridCenter + new Vector2(_widthShift / 2f, -_heightShift / 2f);

                var brick = Instantiate(_prefab, spawnPosition, Quaternion.identity);
                brick.Init(_audio, _particles, _camera, type);
                brick.GetComponent<SpriteRenderer>().sortingOrder = orderInLayer;
                _remainingBricks.Add(brick);
                OnRemainingBricksChanged?.Invoke(_remainingBricks.Count);
            }
        }
    }

    public void Clear()
    {
        foreach (var brick in _remainingBricks)
            Destroy(brick.gameObject);

        _remainingBricks.Clear();

        OnRemainingBricksChanged?.Invoke(_remainingBricks.Count);
    }

    private void BrickDestroyed(Brick brick)
    {
        Storage.AddDestroyedBrick();
        _remainingBricks.Remove(brick);
        OnRemainingBricksChanged?.Invoke(_remainingBricks.Count);

        if (_remainingBricks.Count == 0)
            OnBricksFinished?.Invoke();
    }
}
