public class State
{
    protected readonly StateMachine _stateMachine;

    protected State(StateMachine stateMachine)
    {
        _stateMachine = stateMachine;
    }

    public virtual void EnterState()    {}

    public virtual void UpdateLogic()   {}

    public virtual void ExitState()     {}

}
