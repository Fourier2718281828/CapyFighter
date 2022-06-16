using UnityEngine;

public class Unit : MonoBehaviour
{
    private UnitData _data;

    private Animator _animator;
    private Fighter _fighter;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _fighter = GetComponent<Fighter>();
    }

    public void Init(UnitData data)
    {
        _data = data;
        _animator.runtimeAnimatorController = _data.Animator;
    }

    public float MaxHP => _data.MaxHP;
    public float MaxMP => _data.MaxMP;
    public float Damage => _data.Damage;

    #region Component Properties
    public Animator Animator => _animator;
    public Fighter Fighter => _fighter;
    #endregion
}
