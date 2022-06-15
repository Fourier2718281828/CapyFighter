using UnityEngine;

public class PauseState : State
{
    public PauseState(StateMachine stateMachine)
        : base(stateMachine)
    {
    }

    public override void EnterState()
    {
        base.EnterState();
        Debug.Log("Pause Entered");
    }

    public override void ExitState()
    {
        base.ExitState();
        Debug.Log("Pause Exited");
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
    }
}
