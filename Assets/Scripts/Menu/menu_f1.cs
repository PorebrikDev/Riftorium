using UnityEngine;
using Zenject;

public class menu_f1 : MonoBehaviour
{
    [Inject] private readonly InputService _input;

    private void Awake()
    {
        _input.OnF1Started += OpenClose;
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        _input.OnF1Started -= OpenClose;
    }

    public void OpenClose()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }
}
