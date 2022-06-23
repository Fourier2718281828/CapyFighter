using System;
using UnityEngine;

public class Task
{
    private EnemyAI _enemyAI;

    private AIObject _taskDoer;
    private AIObject _target;
    private TaskType _type;
    private bool _isAssigned;
    private Fighter _targetFighter;

    public int Priority { get; private set; }
    public AIObject TaskDoer => _taskDoer;
    public AIObject Target => _target;
    public TaskType Type => _type;

    //The order means
    public enum TaskType
    {
        SkipTurn = 1,
        Move,
        EquipShield,
        Attack,
        SuperAttack,
    }

    public Task(EnemyAI enemyAI, TaskType taskType, AIObject target = null)
    {
        _enemyAI = enemyAI;
        _type = taskType;
        Priority = (int)taskType;
        _isAssigned = false;
        _targetFighter = target?.GetComponent<Fighter>();

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
            case TaskType.SkipTurn:
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
            case TaskType.SkipTurn:
                break;
            default:
                throw new InvalidOperationException("No such task type.");
        }
    }

    public void Assign(AIObject obj)
    {
        _taskDoer = obj;
        _isAssigned = true;
    }

    public bool IsAssigned() => _isAssigned;

    public float PriorityMultiplier()
    {
        switch(Type)
        {
            case TaskType.Attack:
                return AttackPriorityMultiplier();
            case TaskType.SuperAttack:
                return AttackPriorityMultiplier();
            case TaskType.EquipShield:
                return ShieldEquipPriorityMultiplier();
            case TaskType.Move:
                return 1f;
            case TaskType.SkipTurn:
                return 1f;
            default:
                return 1f;
        }
    }

    private float AttackPriorityMultiplier()
    {
        var weightedHPComponent = _enemyAI.HeroHPWeight * _targetFighter.HPPercentage();
        var weightedMPComponent = _enemyAI.HeroMPWeight * _targetFighter.MPPercentage();
        var weighter = _enemyAI.HeroHPWeight + _enemyAI.HeroMPWeight;
        var weighted = (weightedHPComponent + weightedMPComponent) / weighter;
        return 1f + weighted;
    }

    private float ShieldEquipPriorityMultiplier()
    {
        //var maxPossibleDamageToReceive = 0;
        //int currHeroDamage;
        //foreach(var hero in _enemyAI.Heros)
        //{
        //    currHeroDamage = Mathf.Max(hero.Fighter.GetAttackDamageTo())
        //}
        return 1.5f;
    }
}