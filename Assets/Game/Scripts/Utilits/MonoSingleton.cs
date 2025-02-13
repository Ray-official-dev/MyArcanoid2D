using UnityEngine;

public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T Instance { get; private set; }

    protected virtual void Awake()
    {
        if (Instance != this & Instance != null)
            DestroyImmediate(Instance.gameObject);

        Instance = this as T;
    }
}