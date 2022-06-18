using UnityEngine;

[CreateAssetMenu(fileName = "UnitData", menuName = "UnitData")]
public class UnitData : ScriptableObject
{
    [SerializeField] private RuntimeAnimatorController _animator;
    [SerializeField] private float _maxHP;
    [SerializeField] private float _maxMP;
    [SerializeField] private float _attackDamage;
    [SerializeField] private float _attackMana;
    [SerializeField] private float _superAttackDamage;
    [SerializeField] private float _superAttackMana;

    public RuntimeAnimatorController Animator => _animator;
    public float MaxHP => _maxHP;
    public float MaxMP => _maxMP;
    public float AttackDamage => _attackDamage;
    public float AttackMana => _attackMana;
    public float SuperAttackDamage => _superAttackDamage;
    public float SuperAttackMana => _superAttackMana;
}   