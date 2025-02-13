using UnityEngine;
using TMPro;

[RequireComponent(typeof(TMP_Text))]
[DisallowMultipleComponent]
public class DestroyedBricksShower : MonoBehaviour
{
    [SerializeField] string _prefix;

    private void Start()
    {
        GetComponent<TMP_Text>().text = _prefix + Storage.GetDestroyedBricks().ToString();
    }
}