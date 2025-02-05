using System;

public class EventBus : MonoSingleton<EventBus>
{
    public event Action<Brick> OnBrickDestroyed;
    public event Action<Effect> OnEffectAdded;
    public event Action OnLevelRestarted;

    protected override void Awake()
    {
        base.Awake();
    }

    public void BrickDestroyed(Brick brick)
        => OnBrickDestroyed?.Invoke(brick);

    public void EffectAdded(Effect effect)
        => OnEffectAdded?.Invoke(effect);

    public void LevelRestarted()
        => OnLevelRestarted?.Invoke();
}