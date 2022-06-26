using UnityEngine;

public class WinState : State
{
    private readonly CombatController _controller;
    private readonly VictoryCanvasShower _victoryShower;
    //private readonly AudioManager _audioManager;

    public WinState(StateMachine stateMachine)
        : base(stateMachine)
    {
        _controller = (CombatController)stateMachine;
        _victoryShower = _controller.GetComponent<VictoryCanvasShower>();
        //_audioManager = _controller.GetComponent<AudioManager>();

    }

    public override void EnterState()
    {
        base.EnterState();
        //_audioManager.PlayVictorySound();
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
