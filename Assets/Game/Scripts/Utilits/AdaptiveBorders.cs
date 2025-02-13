using UnityEngine;

public class AdaptiveBorders : MonoBehaviour
{
    [SerializeField] private BoxCollider2D _left;
    [SerializeField] private BoxCollider2D _right;
    [SerializeField] private BoxCollider2D _top;
    [SerializeField] private BoxCollider2D _bottom;

    [SerializeField] private float _thickness = 1;
    [SerializeField] private float _wallShift = 1;
    [SerializeField] private float _topShift = 1;

    private Camera _mainCamera;

    private void Awake()
    {
        _mainCamera = Camera.main;
        UpdateBorders();
    }

    private void UpdateBorders()
    {
        float screenHeight = _mainCamera.orthographicSize * 2.0f;
        float screenWidth = screenHeight * _mainCamera.aspect;

        if (_left != null)
        {
            _left.size = new Vector2(_thickness, screenHeight);
            _left.offset = new Vector2(-screenWidth / 2 - _thickness / 2 + _wallShift, 0);
        }

        if (_right != null)
        {
            _right.size = new Vector2(_thickness, screenHeight);
            _right.offset = new Vector2(screenWidth / 2 + _thickness / 2 - _wallShift, 0);
        }

        if (_top != null)
        {
            _top.size = new Vector2(screenWidth, _thickness);
            _top.offset = new Vector2(0, screenHeight / 2 + _thickness / 2 - _topShift);
        }

        if (_bottom != null)
        {
            _bottom.size = new Vector2(screenWidth, _thickness);
            _bottom.offset = new Vector2(0, -screenHeight / 2 - _thickness / 2);
        }
    }
}
