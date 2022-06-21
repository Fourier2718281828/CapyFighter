using UnityEngine;

public class HeroTurnState : PausableState
{
    private readonly CombatController _controller;
    private bool _theTurnIsUsed;

    public HeroTurnState(CombatController stateMachine)
        : base(stateMachine)
    {
        _controller = stateMachine;
        _theTurnIsUsed = true;
    }

    public override void EnterState()
    {
        base.EnterState();
        _theTurnIsUsed = false;
        //Debug.Log("HeroTurn entered");
    }

    public override void ExitState()
    {
        base.ExitState();
        //Debug.Log("HeroTurn exited");
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();

        if (!_controller.AreSlotsSelected() || _theTurnIsUsed)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            Fighter attackingFighter = _controller.GetHeroFighterAtSlot(_controller.SelectedHeroSlot);
            Fighter victimFighter = _controller.GetEnemyFighterAtSlot(_controller.SelectedEnemySlot);
            attackingFighter.Attack(victimFighter);
            _controller.SwitchStateInSeconds(_controller.EnemyTurnState, _controller.TurnDurationSeconds);
            _controller.RefreshSelectedSlots();
            _theTurnIsUsed = true;
        }
        else if(Input.GetKey(KeyCode.S))
        {
            Fighter attackingFighter = _controller.GetHeroFighterAtSlot(_controller.SelectedHeroSlot);
            Fighter victimFighter = _controller.GetEnemyFighterAtSlot(_controller.SelectedEnemySlot);
            attackingFighter.SuperAttack(victimFighter);
            _controller.SwitchStateInSeconds(_controller.EnemyTurnState, _controller.TurnDurationSeconds);
            _controller.RefreshSelectedSlots();
            _theTurnIsUsed = true;
        }
    }
}