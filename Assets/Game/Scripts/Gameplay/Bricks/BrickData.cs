using System;
using UnityEngine;

[Serializable]
public class BrickData
{
    public int HitPoints => _hitPoints;
    public Sprite Sprite => _sprite;
    public Color Color => _color;
    public Brick.BrickType Type => _type;

    [SerializeField] int _hitPoints;
    [SerializeField] Sprite _sprite;
    [SerializeField] Color _color;
    [SerializeField] private Brick.BrickType _type;
}