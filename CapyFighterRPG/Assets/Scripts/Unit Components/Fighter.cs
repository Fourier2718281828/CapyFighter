using UnityEngine;
using System;

public class Fighter : MonoBehaviour
{
    private Unit _unit;
    private float _currentHP;
    private float _currentMP;

    #region Events
    public event Action<float, float> OnDamageReceived;
    public event Action<float> OnAttacked;
    public event Action<float> OnSuperAttacked;
    public event Action OnDied;
    #endregion

    private void Awake()
    {
        _unit = GetComponent<Unit>();
    }

    private void Start()
    {
        _currentHP = _unit.MaxHP;
        _currentMP = _unit.MaxMP;
    }

    public void ReceiveDamage(float damage)
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
        victim.ReceiveDamage(_unit.AttackDamage);
        OnAttacked?.Invoke(MPPercentage());
    }

    public void SuperAttack(Fighter victim)
    {
        SpendMana(_unit.SuperAttackMana);
        victim.ReceiveDamage(_unit.SuperAttackDamage);
        OnSuperAttacked?.Invoke(MPPercentage());
    }

    public void Die()
    {
        OnDied?.Invoke();
    }

    public bool IsDead() => _currentHP == 0f;

    public float SpendHealth(float hp)
    {
        return _currentHP = hp >= _currentHP ? 0f : _currentHP - hp;
    }

    public float SpendMana(float mp)
    {
        return _currentMP = mp >= _currentMP ? 0f : _currentMP - mp;
    }

    public float HPPercentage() => _currentHP / _unit.MaxHP;

    public float MPPercentage() => _currentMP / _unit.MaxMP;
}