using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;
using System;
using TMPro;

[RequireComponent(typeof(Image))]
public class LevelIcon : MonoBehaviour, IPointerClickHandler
{
    public event Action<int> OnClicked;

    [SerializeField] Sprite _defaultSprite;
    [SerializeField] Sprite _lockSprite;
    [SerializeField] TMP_Text _levelText;

    private int _levelIndex;
    private bool _isLocked;

    public void Init(int levelIndex, bool isLocked)
    {
        _levelIndex = levelIndex;
        _isLocked = isLocked;

        GetComponent<Image>().sprite = _isLocked ? _lockSprite : _defaultSprite;

        if (_isLocked)
            Destroy(_levelText);
        else
            _levelText.text = (_levelIndex + 1).ToString();
    }

    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        if (_isLocked)
            return;

        OnClicked?.Invoke(_levelIndex);
    }
}
