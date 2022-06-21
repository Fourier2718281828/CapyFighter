using UnityEngine;
using System;

public class Fighter : MonoBehaviour
{
    private Unit _unit;
    private CombatController _controller;
    private FieldMetricSpace _fieldMetricSpace;
    private int _currentHP;
    private int _currentMP;

    #region Events
    public event Action<float, float> OnDamageReceived;
    public event Action<float> OnAttacked;
    public event Action<float> OnSuperAttacked;
    public event Action OnDied;
    #endregion

    private void Awake()
    {
        _unit = GetComponent<Unit>();
        GameObject CombatController = GameObject.FindGameObjectWithTag("CombatController");
        _controller = CombatController.GetComponent<CombatController>();
        _fieldMetricSpace = _controller.GetComponent<FieldMetricSpace>();
    }

    private void Start()
    {
        _currentHP = _unit.MaxHP;
        _currentMP = _unit.MaxMP;
    }

    public void ReceiveDamage(int damage)
    {
        SpendHealth(damage);

        if (IsDead())
            OnDied?.Invoke();
        else
            OnDamageReceived?.Invoke(HPPercentage(), damage);
    }

    public void Attack(Fighter victim)
    {
        SpendMana(_unit.AttackMana);
        victim.ReceiveDamage(GetAttackDamageTo(victim));
        OnAttacked?.Invoke(MPPercentage());
    }

    public void SuperAttack(Fighter victim)
    {
        SpendMana(_unit.SuperAttackMana);
        victim.ReceiveDamage(GetSuperAttackDamageTo(victim));
        OnSuperAttacked?.Invoke(MPPercentage());
    }

    public void Die()
    {
        OnDied?.Invoke();
    }

    public int GetAttackDamageTo(Fighter victim)
    {
        var thisSlot = _controller.GetUnitSlotByFighter(this);
        var victimSlot = _controller.GetUnitSlotByFighter(victim);

        var damageMultiplier = 1 / _fieldMetricSpace.Metric(thisSlot, victimSlot);
        return (int)(damageMultiplier * _unit.AttackDamage);
    }

    public int GetSuperAttackDamageTo(Fighter victim)
    {
        var thisSlot = _controller.GetUnitSlotByFighter(this);
        var victimSlot = _controller.GetUnitSlotByFighter(victim);

        var damageMultiplier = 1 / _fieldMetricSpace.Metric(thisSlot, victimSlot);
        return (int)(damageMultiplier * _unit.SuperAttackDamage);
    }

    public bool IsDead() => _currentHP < Mathf.Epsilon;

    public float SpendHealth(int hp)
    {
        return _currentHP = hp >= _currentHP ? 0 : _currentHP - hp;
    }

    public float SpendMana(int mp)
    {
        return _currentMP = mp >= _currentMP ? 0 : _currentMP - mp;
    }

    public float HPPercentage() => (float)_currentHP / _unit.MaxHP;

    public float MPPercentage() => (float)_currentMP / _unit.MaxMP;
}