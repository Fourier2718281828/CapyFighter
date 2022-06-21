using UnityEngine;

public class HeroTurnState : PausableState
{
    private readonly CombatController _controller;
    private readonly Spawner _spawner;
    private bool _theTurnIsUsed;

    public HeroTurnState(CombatController stateMachine)
        : base(stateMachine)
    {
        _controller = stateMachine;
        _spawner = _controller.GetComponent<Spawner>();
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

        if (_theTurnIsUsed) return;

        if (!_controller.IsHeroSlotSelected()) return;

        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            MoveUp();
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            MoveDown();
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MoveLeft();
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            MoveRight();
        }

        if (!_controller.AreSlotsSelected()) return;

        if (Input.GetKeyDown(KeyCode.A))
        {
            Attack();
        }
        else if(Input.GetKey(KeyCode.S))
        {
            SuperAttack();
        }
    }

    private void Attack()
    {
        Fighter attackingFighter = _controller.GetHeroFighterAtSlot(_controller.SelectedHeroSlot);
        Fighter victimFighter = _controller.GetEnemyFighterAtSlot(_controller.SelectedEnemySlot);
        attackingFighter.Attack(victimFighter);
        _controller.SwitchStateInSeconds(_controller.EnemyTurnState, _controller.TurnDurationSeconds);
        _controller.RefreshSelectedSlots();
        _theTurnIsUsed = true;
    }

    private void SuperAttack()
    {
        Fighter attackingFighter = _controller.GetHeroFighterAtSlot(_controller.SelectedHeroSlot);
        Fighter victimFighter = _controller.GetEnemyFighterAtSlot(_controller.SelectedEnemySlot);
        attackingFighter.SuperAttack(victimFighter);
        _controller.SwitchStateInSeconds(_controller.EnemyTurnState, _controller.TurnDurationSeconds);
        _controller.RefreshSelectedSlots();
        _theTurnIsUsed = true;
    }

    private void MoveUp()
    {
        var indexDisplacement = _spawner.HeroSlotColsCount;
        VerticalMoveByDisplacement(indexDisplacement);
    }
    private void MoveDown()
    {
        var indexDisplacement = -_spawner.HeroSlotColsCount;
        VerticalMoveByDisplacement(indexDisplacement);
    }
    private void MoveLeft()
    {
        var displacement = 1;
        HorizontalMoveByDisplacement(displacement);
    }
    private void MoveRight()
    {
        var indexDisplacement = -1;
        HorizontalMoveByDisplacement(indexDisplacement);
    }

    private void VerticalMoveByDisplacement(int displacement)
    {
        var selectedHero = _controller.GetHeroAtSlot(_controller.SelectedHeroSlot);
        var mover = selectedHero.GetComponent<Mover>();
        var thisSlot = _controller.GetHeroSlot(selectedHero);
        var nextSlot = thisSlot + displacement;

        if (_controller.IsHeroSlotOccupied(nextSlot) || 
            nextSlot >= _spawner.HeroSlotCount || 
            nextSlot < 0)
        {
            return;
        }

        mover.MoveToSlot(nextSlot);
        _controller.SwitchState(_controller.MovingState);
    }

    private void HorizontalMoveByDisplacement(int displacement)
    {
        var selectedHero = _controller.GetHeroAtSlot(_controller.SelectedHeroSlot);
        var mover = selectedHero.GetComponent<Mover>();
        var thisSlot = _controller.GetHeroSlot(selectedHero);
        var nextSlot = thisSlot + displacement;

        if (_controller.GetRowOfHeroSlot(thisSlot) != _controller.GetRowOfHeroSlot(nextSlot) ||
            _controller.IsHeroSlotOccupied(nextSlot) ||
            nextSlot < 0)
        {
            return;
        }

        mover.MoveToSlot(nextSlot);
        _controller.SwitchState(_controller.MovingState);
    }
}