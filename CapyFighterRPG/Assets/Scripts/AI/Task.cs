using System;

public class Task
{
    //private int _priorityModifier;
    private AIObject _taskDoer;
    private AIObject _target;
    private TaskType _type;

    public int Priority { get; private set; }
    public AIObject TaskDoer => _taskDoer;
    public TaskType Type => _type;

    public enum TaskType
    {
        SuperAttack = 1,
        Attack,
        EquipShield,
        Move,
    }

    public Task(TaskType taskType, AIObject target = null)
    {
        _type = taskType;
        Priority = (int)taskType;

        switch (taskType)
        {
            case TaskType.Attack:
                _target = target ?? throw new InvalidOperationException("Attack requires a target.");
                break;
            case TaskType.SuperAttack:
                _target = target ?? throw new InvalidOperationException("Super attack requires a target.");
                break;
            case TaskType.EquipShield:
                break;
            case TaskType.Move:
                break;
            default:
                throw new InvalidOperationException("No such task type.");
        }
    }

    public void Do()
    {
        if (!IsAssigned())
            throw new InvalidOperationException("Doint an unassigned task is impossible.");

        switch(_type)
        {
            case TaskType.Attack:
                _taskDoer.Fighter.Attack(_target.Fighter);
                break;
            case TaskType.SuperAttack:
                _taskDoer.Fighter.SuperAttack(_target.Fighter);
                break;
            case TaskType.EquipShield:
                _taskDoer.Fighter.EquipShield();
                break;
            case TaskType.Move:
                //TODO
                break;
            default:
                throw new InvalidOperationException("No such task type.");
        }
    }

    public void Assign(AIObject obj) => _taskDoer = obj ?? throw new ArgumentNullException("Cannot assign null as doer.");

    public bool IsAssigned() => _taskDoer != null;

    public int PriorityModifier => 0;
}