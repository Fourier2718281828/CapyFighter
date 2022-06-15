using UnityEngine;

public class EnemyTurnState : PausableState
{
    public EnemyTurnState(CombatController stateMachine)
        : base(stateMachine)
    {
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
    }
}
