using UnityEngine;

public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T Instance { get; private set; }

    protected virtual void Awake()
    {
        if (Instance is null)
            Instance = this as T;
        else if (Instance != this)
            DestroyImmediate(Instance);
    }
}