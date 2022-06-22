using UnityEngine;
using System;

public class Unit : MonoBehaviour
{
    #region Fields
    private UnitData _data;
    private Animator _animator;
    private Fighter _fighter;
    #endregion

    #region MonoBehaviour Methods
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _fighter = GetComponent<Fighter>();
    }

    private void OnEnable()
    {
        _fighter.OnAttacked         += _ => _animator.Play("Attack");
        _fighter.OnDamageReceived   += (p, d) => _animator.Play("Hurt");
        _fighter.OnSuperAttacked    += _ => _animator.Play("SuperAttack");
    }
    #endregion

    #region Custom Methods
    public void Init(UnitData data)
    {
        _data = data;
        _animator.runtimeAnimatorController = _data.Animator;
    }
    #endregion

    #region Properties
    public Sprite AvatarIcon => _data.AvatarIcon;
    public int MaxHP => _data.MaxHP;
    public int MaxMP => _data.MaxMP;
    public int AttackDamage => _data.AttackDamage;
    public int AttackManaCost => _data.AttackManaCost;
    public int SuperAttackDamage => _data.SuperAttackDamage;
    public int SuperAttackManaCost => _data.SuperAttackManaCost;
    public int MaxShieldHP => _data.MaxShieldHP;
    public int ShieldManaCost => _data.ShieldManaCost;
    public int ManaRegainRate => _data.ManaRegainRate;
    #endregion

    #region Component Properties
    public Animator Animator => _animator;
    #endregion
}
