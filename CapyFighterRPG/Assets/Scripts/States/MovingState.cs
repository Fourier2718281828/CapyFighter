using UnityEngine;

public class MovingState : PausableState
{
    private readonly CombatController _controller;
    private Mover _currentlyMovingUnit;

    public MovingState(CombatController stateMachine)
        : base(stateMachine)
    {
        _controller = stateMachine;
    }

    public override void EnterState()
    {
        base.EnterState();
        var selectedUnit =  _controller.GetHeroAtSlot(_controller.SelectedHeroSlot)
                            ??
                            _controller.GetEnemyAtSlot(_controller.SelectedEnemySlot);

        _currentlyMovingUnit = selectedUnit.GetComponent<Mover>();
    }

    public override void ExitState()
    {
        base.ExitState();
        //Debug.Log("HeroTurn exited");
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();

        if (!_currentlyMovingUnit.IsMoving)
            _controller.SwitchState(_controller.PreviousState);
    }
}