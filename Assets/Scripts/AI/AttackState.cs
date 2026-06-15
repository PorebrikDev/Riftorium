using UnityEngine;

public class AttackState : IEnemyState
{
    private EnemyBrain _brain;
    private EnemyDateSO _dateSO;

    public AttackState(EnemyBrain brain, EnemyDateSO dateSO)
    {
        _brain = brain;
        _dateSO = dateSO;
    }

    public void Enter()
    {
        _brain.Combat.SetupAttack(_dateSO);

        _brain.Animator.SetTrigger(_dateSO.AnimatorTrigger);
    }

    public void Tick()
    {

    }

    public void Exit()
    {

    }
}
