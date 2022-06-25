using UnityEngine;

public class LossState : State
{
    private readonly CombatController _controller;
    private readonly LossCanvasShower _lossCanvas;

    public LossState(StateMachine stateMachine)
        : base(stateMachine)
    {
        _controller = (CombatController)stateMachine;
        _lossCanvas = _controller.GetComponent<LossCanvasShower>();
    }

    public override void EnterState()
    {
        base.EnterState();
        Debug.Log("Loss");
        _lossCanvas.ShowLossCanvas();
    }

    public override void ExitState()
    {
        base.ExitState();
        Debug.Log("Loss Exited");
        _lossCanvas.HideLossCanvas();
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
    }
}
