using UnityEngine;
using TMPro;

[RequireComponent(typeof(TMP_Text))]
public class AppVersionShower : MonoBehaviour
{
    private void Start()
    {
        GetComponent<TMP_Text>().text = $"v {Application.version}";
    }
}
