using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class HpBar : MonoBehaviour
{
    [Inject] private readonly Player _player;

    private Image image;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    private void Start()
    {
        _player.OnChangeHp += UpdateHpBur;
        image.fillAmount = _player.MaxHealth;
    }

    public void UpdateHpBur(float HP)
    {
        image.fillAmount = HP / _player.MaxHealth;
    }

    private void OnDisable()
    {
        _player.OnChangeHp -= UpdateHpBur;
    }
}