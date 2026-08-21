using TMPro;
using UnityEngine;

public class PrefabTolk : MonoBehaviour
{
    private TMP_Text _text;

    private void Awake()
    {
        _text = GetComponent<TMP_Text>();
    }

    public void SetText(string text) => _text.text = text;

}
