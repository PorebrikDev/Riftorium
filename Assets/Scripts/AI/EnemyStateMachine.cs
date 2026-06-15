public class EnemyStateMachine
{
    private IEnemyState _currentState;

    public void ChangeState(IEnemyState newState)
    {
        _currentState?.Exit();

        _currentState = newState;

        _currentState.Enter();
    }

    public void Tick()
    { 
    _currentState?.Tick();
    }
}
