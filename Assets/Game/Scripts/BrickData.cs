using System;
using UnityEngine;

[Serializable]
public class BrickData
{
    public int HitPoints => _hitPoints;
    public Sprite Sprite => _sprite;
    public Color Color => _color;

    [SerializeField] int _hitPoints;
    [SerializeField] Sprite _sprite;
    [SerializeField] Color _color;
}