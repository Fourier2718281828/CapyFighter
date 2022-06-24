using UnityEngine;

public class WinState : State
{
    private readonly CombatController _controller;
    private readonly VictoryCanvasShower _victoryShower;

    public WinState(StateMachine stateMachine)
        : base(stateMachine)
    {
        _controller = (CombatController)stateMachine;
        _victoryShower = _controller.GetComponent<VictoryCanvasShower>();
    }

    public override void EnterState()
    {
        base.EnterState();
        Debug.Log("WinState Entered");
        _victoryShower.ShowVictoryCanvas();
    }

    public override void ExitState()
    {
        base.ExitState();
        Debug.Log("WinState Exited");
        _victoryShower.HideVictoryCanvas();
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
    }
}
