using UnityEngine;

[CreateAssetMenu(fileName = "UnitData", menuName = "UnitData")]
public class UnitData : ScriptableObject
{
    [SerializeField] private RuntimeAnimatorController _animator;
    [SerializeField] private Sprite _avatarIcon;
    [SerializeField] private int _maxHP;
    [SerializeField] private int _maxMP;
    [SerializeField] private int _attackDamage;
    [SerializeField] private int _attackMana;
    [SerializeField] private int _superAttackDamage;
    [SerializeField] private int _superAttackMana;

    public RuntimeAnimatorController Animator => _animator;
    public Sprite AvatarIcon => _avatarIcon;
    public int MaxHP => _maxHP;
    public int MaxMP => _maxMP;
    public int AttackDamage => _attackDamage;
    public int AttackMana => _attackMana;
    public int SuperAttackDamage => _superAttackDamage;
    public int SuperAttackMana => _superAttackMana;
}   