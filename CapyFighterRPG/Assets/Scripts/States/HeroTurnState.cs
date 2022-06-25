using UnityEngine;

public class HeroTurnState : PausableState
{
    private readonly CombatController _controller;
    private readonly Spawner _spawner;
    private readonly MessageTextShower _messageTextShower;
    private readonly ControlSet _controlSet;
    private bool _theTurnIsUsed;

    private bool IsFaded => _messageTextShower.IsFaded;
    public HeroTurnState(CombatController stateMachine)
        : base(stateMachine)
    {
        _controller = stateMachine;
        _spawner = _controller.GetComponent<Spawner>();
        _messageTextShower = _controller.GetComponent<MessageTextShower>();
        _controlSet = _controller.GetComponent<ControlSet>();
        _theTurnIsUsed = true;

    }

    public override void EnterState()
    {
        base.EnterState();
        Debug.Log("Hero turn");
        _theTurnIsUsed = false;
        _controller.RefreshSelectedSlots();
        _messageTextShower.ShowMessage("Your Turn", _controller.MessageUnfadeDuration, _controller.MessageFadeDuration);

        foreach (var fighter in _controller.HerosToFighters.Values)
        {
            fighter.RegainMana();
        }
    }

    public override void ExitState()
    {
        base.ExitState();
        Debug.Log("Hero turn exited");
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();

        if (_theTurnIsUsed || !IsFaded) return;

        if (!_controller.IsHeroSlotSelected()) return;

        SubscribeEventsToControls();

        //Moving
        if (Input.GetKeyDown(KeyCode.UpArrow))
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

        //Shield equipment
        if (Input.GetKey(KeyCode.D))
        {
            EquipShield();
        }

        if (!_controller.AreSlotsSelected()) return;

        if (Input.GetKeyDown(KeyCode.A))
        {
            Attack();
        }
        else if (Input.GetKey(KeyCode.S))
        {
            SuperAttack();
        }
    }

    private void SubscribeEventsToControls()
    {
        _controlSet.AttackButton.onClick.RemoveAllListeners();
        _controlSet.AttackButton.onClick.AddListener(() => Attack());
        _controlSet.SuperAttackButton.onClick.AddListener(() => SuperAttack());
        _controlSet.EquipShieldButton.onClick.AddListener(() => EquipShield());
        _controlSet.MoveUpButton.onClick.AddListener(() => MoveUp());
        _controlSet.MoveDownButton.onClick.AddListener(() => MoveDown());
    }

    private void Attack()
    {
        Debug.Log($"Selected slots : {_controller.SelectedHeroSlot}, {_controller.SelectedEnemySlot}");
        Fighter attackingFighter = _controller.GetHeroFighterAtSlot(_controller.SelectedHeroSlot);

        if (attackingFighter.CanAttack())
        {
            Fighter victimFighter = _controller.GetEnemyFighterAtSlot(_controller.SelectedEnemySlot);
            attackingFighter.Attack(victimFighter);
            if (_controller.EnemyCount == 0)
                _controller.SwitchStateInSeconds(_controller.WinState, _controller.TurnDurationSeconds);
            else
                _controller.SwitchStateInSeconds(_controller.EnemyTurnState, _controller.TurnDurationSeconds);
            _theTurnIsUsed = true;
        }
        else
        {
            //TODO some message like "Not enough mana"
        }
    }

    private void SuperAttack()
    {
        Fighter attackingFighter = _controller.GetHeroFighterAtSlot(_controller.SelectedHeroSlot);

        if(attackingFighter.CanSuperAttack())
        {
            Fighter victimFighter = _controller.GetEnemyFighterAtSlot(_controller.SelectedEnemySlot);
            attackingFighter.SuperAttack(victimFighter);
            if (_controller.EnemyCount == 0)
                _controller.SwitchStateInSeconds(_controller.WinState, _controller.TurnDurationSeconds);
            else
                _controller.SwitchStateInSeconds(_controller.EnemyTurnState, _controller.TurnDurationSeconds);
            _theTurnIsUsed = true;
        }
        else
        {
            //TODO some message like "Not enough mana"
        }
    }

    private void EquipShield()
    {
        Fighter fighterToGetEquiped = _controller.GetHeroFighterAtSlot(_controller.SelectedHeroSlot);
        if (fighterToGetEquiped.CanEquipShield())
        {
            fighterToGetEquiped.EquipShield();
            _controller.SwitchStateInSeconds(_controller.EnemyTurnState, _controller.TurnDurationSeconds);
            _theTurnIsUsed = true;
        }
        else
        {
            //TODO make some message like "Not enough mana"
        }
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