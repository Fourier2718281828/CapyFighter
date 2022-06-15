using UnityEngine;

[CreateAssetMenu(fileName = "UnitData", menuName = "UnitData")]
public class UnitData : ScriptableObject
{
    [Tooltip("Animator tip")]
    [SerializeField] private RuntimeAnimatorController _animator;

    public RuntimeAnimatorController Animator => _animator;
}
