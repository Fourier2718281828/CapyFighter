using UnityEngine;

public class EnemyTurnState : PausableState
{
    private readonly CombatController _controller;
    private readonly EnemyAI _enemyAI;
    public EnemyTurnState(CombatController stateMachine)
        : base(stateMachine)
    {
        _controller = stateMachine;
        _enemyAI = _controller.GetComponent<EnemyAI>();
    }

    public override void EnterState()
    {
        base.EnterState();
        //Debug.Log("EnemyTurn entered");
    }

    public override void ExitState()
    {
        base.ExitState(); 
        //Debug.Log("EnemyTurn exited");
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();

        Task taskToDo = _enemyAI.NextTurnTask();
        taskToDo.Do();

        //Fighter enemy = _controller.GetEnemyFighterAtSlot(0);
        //Fighter victim = _controller.GetHeroFighterAtSlot(0);

        //enemy.Attack(victim);

        _controller.SwitchState(_controller.HeroTurnState);
    }
}