using UnityEngine;

[CreateAssetMenu(fileName = "Effect", menuName = Constans.SOPath + "New Effect")]
public class EffectConfig : ScriptableObject
{
    public Effect.EffectType Type => _type;
    public Sprite Sprite => _sprite;
    public float Duration => _duration;

    [SerializeField] Effect.EffectType _type;
    [SerializeField] Sprite _sprite;
    [SerializeField] float _duration;
}
