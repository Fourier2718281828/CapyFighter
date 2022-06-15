using UnityEngine;

public abstract class StateMachine : MonoBehaviour
{
    public State CurrentState { get; private set; }

    protected void SetUp()
    {
        CurrentState = InitialState();
        CurrentState?.EnterState();
    }

    protected void UpdateLogic()
    {
        CurrentState?.UpdateLogic();
    }

    protected abstract State InitialState();

    public void SwitchState(State newState)
    {
        CurrentState.ExitState();
        CurrentState = newState;
        CurrentState.EnterState();
    }
}