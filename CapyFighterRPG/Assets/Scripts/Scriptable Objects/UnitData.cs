using UnityEngine;

[CreateAssetMenu(fileName = "UnitData", menuName = "UnitData")]
public class UnitData : ScriptableObject
{
    [SerializeField] private RuntimeAnimatorController _animator;
    [SerializeField] private float _maxHP;
    [SerializeField] private float _maxMP;
    [SerializeField] private float _damage;

    public RuntimeAnimatorController Animator => _animator;
    public float MaxHP => _maxHP;
    public float MaxMP => _maxMP;
    public float Damage => _damage;
}
