using UnityEngine;

public class AppSettings : MonoBehaviour
{
    [SerializeField] int _targetFrameRate;

    private void Awake()
    {
        Application.targetFrameRate = _targetFrameRate;
    }
}
