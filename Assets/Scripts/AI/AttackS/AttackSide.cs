using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/Attask Side")]
public class AttackSide : EnemyDateSO
{
    public override bool CanEnter(EnemyBrain brain)
    {
        if (brain.IsBusy) return false;

        if (Time.time < brain.NextAttackTime) return false;

        if (brain.IsPlayerLeft || brain.IsPlayerRight)
            return true;

        return false;
    }



    public override void Execute(EnemyBrain brain)
    {
        Debug.Log("ATTACK SIDE");
        brain.NextAttackTime = Time.time + Cooldown;

        brain.Combat.SetupAttack(this);
        brain.Animator.SetTrigger(AnimatorTrigger);
    }
}
