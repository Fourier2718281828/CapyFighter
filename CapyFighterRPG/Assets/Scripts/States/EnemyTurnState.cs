public class EnemyTurnState : PausableState
{
    private readonly CombatController _controller;
    private readonly EnemyAI _enemyAI;
    private bool _theTurnIsUsed;

    public EnemyTurnState(CombatController stateMachine)
        : base(stateMachine)
    {
        _controller = stateMachine;
        _enemyAI = _controller.GetComponent<EnemyAI>();
        _theTurnIsUsed = true;
    }

    public override void EnterState()
    {
        base.EnterState();
        _theTurnIsUsed = false;

        foreach (var fighter in _controller.EnemiesToFighters.Values)
        {
            fighter.RegainMana();
        }
    }

    public override void ExitState()
    {
        base.ExitState(); 
        //Debug.Log("EnemyTurn exited");
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();

        if (_theTurnIsUsed) return;

        Task taskToDo = _enemyAI.NextTurnTask();
        taskToDo.Do();
        if (_controller.HeroCount == 0)
            _controller.SwitchStateInSeconds(_controller.LossState, _controller.TurnDurationSeconds);
        else
            _controller.SwitchStateInSeconds(_controller.HeroTurnState, _controller.TurnDurationSeconds);
        _theTurnIsUsed = true;
    }
}