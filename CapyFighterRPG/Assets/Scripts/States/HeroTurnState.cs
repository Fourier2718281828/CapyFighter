using UnityEngine;

public class HeroTurnState : PausableState
{
    private readonly CombatController _controller;
    private bool _turnHasBeenUsed;

    public HeroTurnState(CombatController stateMachine)
        : base(stateMachine)
    {
        _controller = stateMachine;
    }

    public override void EnterState()
    {
        base.EnterState();
        Debug.Log("HeroTurn entered");
        _turnHasBeenUsed = false;
    }

    public override void ExitState()
    {
        base.ExitState();
        Debug.Log("HeroTurn exited");
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();

        if (_controller.SelectedHeroSlot == -1  ||
            _controller.SelectedEnemySlot == -1 ||
            _turnHasBeenUsed)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            //string attackAnimationName =
            //    _controller.HeroSlots[_controller.SelectedHeroSlot].AttackAnimationName;

            //_controller.HeroAnimators[_controller.SelectedHeroSlot].Play(attackAnimationName);

            ////Invoke("SwitchToNext", 3);
            //_controller.SwitchState(_controller.EnemyTurn);
            //_turnHasBeenUsed = true;
            //_controller.RefreshSelected();
        }
    }
}