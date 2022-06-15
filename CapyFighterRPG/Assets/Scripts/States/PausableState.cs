using UnityEngine;

public abstract class PausableState : State
{
    public PausableState(StateMachine stateMachine) 
        : base(stateMachine) {}


    public override void UpdateLogic()
    {
        base.UpdateLogic();
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            _stateMachine.SwitchState(((CombatController)_stateMachine).Pause);
        }
    }
}
