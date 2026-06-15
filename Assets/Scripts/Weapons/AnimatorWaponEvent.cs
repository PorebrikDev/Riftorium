using UnityEngine;
using Zenject;

public class AnimatorWaponEvent : MonoBehaviour
{
    [SerializeField] private AudioClip _clip;

    [Inject] private readonly AudioManager _audioManager;

    private bool isAttacking = false;
    private Tool currentAttackTool;

    public bool CanAttack => !isAttacking;

    public void AnimationStarted()
    {
        isAttacking = true;
    }

    public void Attack_Hero_Start()
    {
        Debug.Log("лог");
        currentAttackTool = ActiveWeapon.Instance.GetActiveWeapon();

        _audioManager.PlaySFXRandomPitch(_clip, 1f, 0.5f, 2f);

        currentAttackTool.BoxOn();
        Invoke(nameof(BoxOff), 0.3f);
    }

    private void BoxOff()
    {
        if (currentAttackTool != null) { currentAttackTool.BoxOff(); }
        isAttacking = false;
    }
}
