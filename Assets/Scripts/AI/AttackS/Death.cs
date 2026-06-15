using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/Death")]
public class Death : EnemyDateSO
{
    public override bool CanEnter(EnemyBrain brain)
    {
        if (brain.EnemyLife.IsDead)
            return true;


        return false;
    }

    public override void Execute(EnemyBrain brain)
    {
        brain.Combat.SetupAttack(this);

        brain.Animator.SetTrigger(AnimatorTrigger);
    }

}

