using UnityEngine;

[CreateAssetMenu(fileName = "UnitData", menuName = "UnitData")]
public class UnitData : ScriptableObject
{
    [SerializeField] private RuntimeAnimatorController _animator;
    [SerializeField] private Sprite _avatarIcon;
    [SerializeField] private int _maxHP;
    [SerializeField] private int _maxMP;
    [SerializeField] private int _attackDamage;
    [SerializeField] private int _attackManaCost;
    [SerializeField] private int _superAttackDamage;
    [SerializeField] private int _superAttackManaCost;
    [SerializeField] private int _maxShieldHP;
    [SerializeField] private int _shieldManaCost;
    [SerializeField] private int _manaRegainRate;

    public RuntimeAnimatorController Animator => _animator;
    public Sprite AvatarIcon => _avatarIcon;
    public int MaxHP => _maxHP;
    public int MaxMP => _maxMP;
    public int AttackDamage => _attackDamage;
    public int AttackManaCost => _attackManaCost;
    public int SuperAttackDamage => _superAttackDamage;
    public int SuperAttackManaCost => _superAttackManaCost;
    public int MaxShieldHP => _maxShieldHP;
    public int ShieldManaCost => _shieldManaCost;
    public int ManaRegainRate => _manaRegainRate;
}   