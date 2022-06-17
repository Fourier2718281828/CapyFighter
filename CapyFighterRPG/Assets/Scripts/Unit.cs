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
        _fighter.OnAttacked += _ => _animator.Play("Attack");
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
    public float MaxHP => _data.MaxHP;
    public float MaxMP => _data.MaxMP;
    public float Damage => _data.Damage;
    #endregion

    #region Component Properties
    public Animator Animator => _animator;
    public Fighter Fighter => _fighter;
    #endregion
}
