using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/AttackDown")]
public class AttackDown : EnemyDateSO
{
    public override bool CanEnter(EnemyBrain brain)
    {
        if (Time.time < brain.NextAttackTime) return false;

        if (brain.IsPlayerDown) return true;

        return false;
    }

    public override void Execute(EnemyBrain brain)
    {
        brain.NextAttackTime = Time.time + Cooldown;

        brain.Combat.SetupAttack(this);

        brain.Animator.SetTrigger(AnimatorTrigger);
    }
}
