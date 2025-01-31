using TMPro;
using UnityEngine;

public class Brick : MonoBehaviour
{
    [SerializeField] private TMP_Text _ui;
    [SerializeField] private int _hitPoints;

    public void TakeDamage(int value)
    {
        _hitPoints -= value;

        if (_hitPoints <= 0)
            Destroy(gameObject);

        //_ui.text = _hitPoints.ToString();
    }
}
