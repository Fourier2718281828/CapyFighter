using UnityEngine;
using System;

public class Fighter : MonoBehaviour
{
    private Unit _unit;
    //private UnitInfo _unitInfo;

    #region Events
    public event Action<float> OnDamageReceived;
    public event Action<float> OnAttacked;
    #endregion

    private void Awake()
    {
        _unit = GetComponent<Unit>();
    }

    public void ReceiveDamage(float damage)
    {
        const float healthPercentage = 0.25f;
        OnDamageReceived?.Invoke(healthPercentage);
    }

    public void Attack(Fighter victim)
    {
        const float manaPercentage = 0.5f;
        OnAttacked?.Invoke(manaPercentage);
        victim.ReceiveDamage(_unit.Damage);
    }
}