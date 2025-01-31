using UnityEngine;

public class BrickSpawner : MonoBehaviour
{
    [SerializeField] private Brick[] _bricks;

    [SerializeField] float _widthShift = 0.37f;
    [SerializeField] float _heightShift = 0.37f;
    [SerializeField] float _topRowY = 4f; 

    public void SpawnBricks(LevelData level)
    {
        Vector2 gridCenter = new Vector2(level.Columns * _widthShift / 2f, level.Rows * _heightShift / 2f);

        
        float yOffset = _topRowY - (-gridCenter.y + _heightShift / 2f);

        for (int row = 0; row < level.Rows; row++)
        {
            for (int col = 0; col < level.Columns; col++)
            {
                int brickType = level.Layout[row].Bricks[col];
                if (brickType == 0) continue;

                Vector2 spawnPos = new Vector2(col * _widthShift, -row * _heightShift + yOffset) - gridCenter + new Vector2(_widthShift / 2f, -_heightShift / 2f);

                Instantiate(_bricks[brickType - 1], spawnPos, Quaternion.identity, transform);
            }
        }
    }
}

