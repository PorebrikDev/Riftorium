using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/Run")]
public class Run : EnemyDateSO
{
    public override bool CanEnter(EnemyBrain brain)
    {
        if (brain.IsPlayerUp)
            return true;


        return false;
    }

    public override void Execute(EnemyBrain brain)
    {
        Debug.Log("RUN");
        brain.Combat.SetupAttack(this);

        brain.Animator.SetTrigger(AnimatorTrigger);
        brain.RunAway();
    }

}

