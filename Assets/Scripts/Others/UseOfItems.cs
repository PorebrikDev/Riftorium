using UnityEngine;
using Zenject;

public class UseOfItems : MonoBehaviour
{
    [Inject] private readonly Player _player;
    [Inject] private readonly AudioManager _audioManager;

    
    public void Use(ItemSO item)
    {
        if (item.isHealing)
        {
            PlayerEffects.instance.HillEfect();
            _player.HpRecovery(item.HealingPower);
            Debug.Log("хилинг на " + item.HealingPower);
        }

        if (item.isTool)
        {
            ChangeWeapon(item);
            UIActiveWeapon.Instance.ChangeIcone(item.icon);
        }

        if (item.IsManaRestore)
        {
            _player.ChangeMana(item.ManaRestorePower);
        }

        if (!item.Clip)  return;

        _audioManager.PlaySFX(item.Clip);
    }

    public void ChangeWeapon(ItemSO tool)
    {
        ActiveWeapon.Instance.SetActiveWeapon(tool.toolIndex);
    }
}
