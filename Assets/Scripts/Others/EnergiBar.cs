using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class EnergiBar : MonoBehaviour
{
    [Inject] private readonly Player _player;

    private Image _image;

    private void Awake()
    {
        _image = GetComponent<Image>();
    }
    private void Start()
    {
        _player.OnChangeMp += ChangeEnergiBar;
    }

    private void OnEnable()
    {
        _image.fillAmount = 1;
    }

    private void OnDestroy()
    {
        _player.OnChangeMp -= ChangeEnergiBar;
    }

    private void ChangeEnergiBar(float Energy)
    {
        _image.fillAmount = Energy / _player.MaxMana;
    }
}
