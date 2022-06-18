using UnityEngine;

public class EnemyTurnState : PausableState
{
    private readonly CombatController _controller;
    public EnemyTurnState(CombatController stateMachine)
        : base(stateMachine)
    {
        _controller = stateMachine;
    }

    public override void EnterState()
    {
        base.EnterState();
        Debug.Log("EnemyTurn entered");
    }

    public override void ExitState()
    {
        base.ExitState(); 
        Debug.Log("EnemyTurn exited");
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();

        Fighter enemy = _controller.GetEnemyFighterAtSlot(0);
        Fighter victim = _controller.GetHeroFighterAtSlot(0);

        enemy.Attack(victim);

        _controller.SwitchState(_controller.HeroTurnState);
    }
}
