using System;

public class PossibleAssignment : IComparable<PossibleAssignment>
{
    private EnemyAI _enemyAI;

    private float _score;
    private AIObject _possibleTaskDoer;
    private Fighter _fighter;

    private float[] _factorValues;
    private float[] _factorWeights;


    public Task TaskToDo { get; private set; }

    public PossibleAssignment(Task taskToDo, AIObject taskDoer, EnemyAI enemyAI)
    {
        if (taskToDo == null)
            throw new ArgumentNullException("Cannot instantiate assignment with a null task.");
        if (taskDoer == null)
            throw new ArgumentNullException("Cannot instantiate assignment with a null task doer.");

        TaskToDo = taskToDo;
        _possibleTaskDoer = taskDoer;
        _enemyAI = enemyAI;
        _fighter = _possibleTaskDoer.Fighter;
        FillTheArrayOfFactors();
        _score = EvaluateScore();
    }

    public void Assign()
    {
        if (TaskToDo.IsAssigned()) return;
        TaskToDo.Assign(_possibleTaskDoer);
        _possibleTaskDoer?.Assign(this);
    }

    private float EvaluateScore()
    {
        float res = TaskToDo.Priority + TaskToDo.PriorityModifier();
        for(int i = 0; i < _factorValues.Length; ++i)
        {
            res += _factorWeights[i] * _factorValues[i];
        }

        return res;
    }

    private void FillTheArrayOfFactors()
    {
        _factorValues = new float[]
        {
            PossibleDamage(),
            PossibleHPSave(),
            PossibleMPCost(),
        };

        _factorWeights = new float[]
        {
            _enemyAI.PossibleDamageWeight,
            _enemyAI.PossibleHPSaveWeight,
            _enemyAI.PossibleMPCostWeight,
        };

        if (_factorValues.Length != _factorValues.Length)
            throw new InvalidOperationException("Factor count must be equal to the count of factor weights.");
    }

    private float PossibleDamage()
    {
        return TaskToDo.Type switch
        {
            Task.TaskType.Attack 
            => _fighter.GetAttackDamageTo(TaskToDo.Target.Fighter), 
            Task.TaskType.SuperAttack 
            => _fighter.GetSuperAttackDamageTo(TaskToDo.Target.Fighter),
            Task.TaskType.EquipShield => 0f,
            Task.TaskType.Move => 0f,
            Task.TaskType.SkipTurn => 0f,
            _ => 0f
        };
    }

    private float PossibleHPSave()
    {
        return TaskToDo.Type switch
        {
            Task.TaskType.Attack => 0f,
            Task.TaskType.SuperAttack => 0f,
            Task.TaskType.EquipShield => _fighter.Unit.MaxShieldHP,
            Task.TaskType.Move => 0f,
            Task.TaskType.SkipTurn => 0f,
            _ => 0f
        };
    }

    private float PossibleMPCost()
    {
        return TaskToDo.Type switch
        {
            Task.TaskType.Attack => _fighter.Unit.AttackManaCost,
            Task.TaskType.SuperAttack => _fighter.Unit.SuperAttackManaCost,
            Task.TaskType.EquipShield => _fighter.Unit.ShieldManaCost,
            Task.TaskType.Move => 0f,
            Task.TaskType.SkipTurn => 0f,
            _ => 0f
        };
    }

    public int CompareTo(PossibleAssignment other)
    {
        return -_score.CompareTo(other._score);
    }
}