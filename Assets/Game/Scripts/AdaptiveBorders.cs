using UnityEngine;

public class AdaptiveBorders : MonoBehaviour
{
    [SerializeField] private BoxCollider2D _left;
    [SerializeField] private BoxCollider2D _right;
    [SerializeField] private BoxCollider2D _top;
    [SerializeField] private BoxCollider2D _bottom;
    [SerializeField] private float _thickness = 1;
    [SerializeField] private float _shift = 1;

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

        _left.size = new Vector2(_thickness, screenHeight);
        _left.offset = new Vector2(-screenWidth / 2 - _thickness / 2 + _shift, 0);

        _right.size = new Vector2(_thickness, screenHeight);
        _right.offset = new Vector2(screenWidth / 2 + _thickness / 2 - _shift, 0);

        _top.size = new Vector2(screenWidth, _thickness);
        _top.offset = new Vector2(0, screenHeight / 2 + _thickness / 2);

        _bottom.size = new Vector2(screenWidth, _thickness);
        _bottom.offset = new Vector2(0, -screenHeight / 2 - _thickness / 2);
    }
}
