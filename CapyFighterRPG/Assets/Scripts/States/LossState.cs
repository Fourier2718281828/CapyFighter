using UnityEngine;

public class LossState : State
{
    private readonly CombatController _controller;
    private readonly PauseShower _pauseShower;

    public LossState(StateMachine stateMachine)
        : base(stateMachine)
    {
        _controller = (CombatController)stateMachine;
        _pauseShower = _controller.GetComponent<PauseShower>();
    }

    public override void EnterState()
    {
        base.EnterState();
        Debug.Log("Loss");
    }

    public override void ExitState()
    {
        base.ExitState();
        Debug.Log("Loss Exited");
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _pauseShower.HidePause();
            _controller.UnpauseCombat();
            _stateMachine.SwitchToPreviousState();
        }
    }
}
