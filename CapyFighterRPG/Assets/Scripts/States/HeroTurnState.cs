using UnityEngine;

public class HeroTurnState : PausableState
{
    private readonly CombatController _controller;

    public HeroTurnState(CombatController stateMachine)
        : base(stateMachine)
    {
        _controller = stateMachine;
    }

    public override void EnterState()
    {
        base.EnterState();
        Debug.Log("HeroTurn entered");
    }

    public override void ExitState()
    {
        base.ExitState();
        Debug.Log("HeroTurn exited");
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();

        if (!_controller.AreSlotsSelected())
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            Fighter attackingFighter = _controller.GetHeroFighterAtSlot(_controller.SelectedHeroSlot);
            Fighter victimFighter = _controller.GetEnemyFighterAtSlot(_controller.SelectedEnemySlot);
            attackingFighter.Attack(victimFighter);
            _controller.SwitchStateInSeconds(_controller.EnemyTurnState, 2f);
            _controller.RefreshSelectedSlots();
        }
        if(Input.GetKey(KeyCode.S))
        {
            Fighter attackingFighter = _controller.GetHeroFighterAtSlot(_controller.SelectedHeroSlot);
            Fighter victimFighter = _controller.GetEnemyFighterAtSlot(_controller.SelectedEnemySlot);
            attackingFighter.SuperAttack(victimFighter);
            _controller.SwitchStateInSeconds(_controller.EnemyTurnState, 2f);
            _controller.RefreshSelectedSlots();
        }
    }
}