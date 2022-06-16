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

        if (!_controller.AreSlotsSelected() || _turnHasBeenUsed)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            GameObject attacker = _controller.GetHeroAtSlot(_controller.SelectedHeroSlot);
            GameObject victim = _controller.GetEnemyAtSlot(_controller.SelectedEnemySlot);
            Unit attackingUnit = _controller.HerosToUnits[attacker];
            Unit victimUnit = _controller.EnemiesToUnits[victim];

            attackingUnit.Fighter.Attack(victimUnit);


            _controller.SwitchState(_controller.EnemyTurn);
            _turnHasBeenUsed = true;
            _controller.RefreshSelectedSlots();
        }
    }
}