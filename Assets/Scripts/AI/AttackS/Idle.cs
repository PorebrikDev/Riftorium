using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/Idle")]
public class Idle : EnemyDateSO
{
    public override bool CanEnter(EnemyBrain brain)
    {
        if (!brain.IsPlayerNear)
            return true;

        return false;
    }

    public override void Execute(EnemyBrain brain)
    {
        brain.Combat.SetupAttack(this);

        brain.Animator.SetTrigger(AnimatorTrigger);
    }
}
