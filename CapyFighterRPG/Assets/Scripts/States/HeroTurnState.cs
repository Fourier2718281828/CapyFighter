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
            Fighter attackingFighter = _controller.GetHeroFighterAtSlot(_controller.SelectedHeroSlot);
            Fighter victimFighter = _controller.GetEnemyFighterAtSlot(_controller.SelectedEnemySlot);

            attackingFighter.Attack(victimFighter);


            _controller.SwitchState(_controller.EnemyTurn);
            _turnHasBeenUsed = true;
            _controller.RefreshSelectedSlots();
        }
    }
}