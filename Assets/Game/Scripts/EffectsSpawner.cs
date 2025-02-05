using UnityEngine;

public class EffectsSpawner  : MonoBehaviour
{
    [SerializeField] Effect _prefab;
    [SerializeField] EffectConfig[] _effects;
    [SerializeField] Brick.BrickType _targerBrick;

    private void OnEnable()
    {
        EventBus.Instance.OnBrickDestroyed += BrickDestroyed;
    }

    private void BrickDestroyed(Brick brick)
    {
        if (brick.InitialType == _targerBrick)
        {
            var index = Random.Range(0, _effects.Length);

            var effect = Instantiate(_prefab, brick.transform.position, Quaternion.identity);
            effect.Setup(_effects[index]);
        }
    }
}
