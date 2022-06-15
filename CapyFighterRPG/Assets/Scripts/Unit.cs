using UnityEngine;

public class Unit : MonoBehaviour
{
    private UnitData _data;

    private Animator _animator;
    public void Init(UnitData data)
    {
        _data = data;
        _animator = GetComponent<Animator>();
        _animator.runtimeAnimatorController = _data.Animator;
    }

    public Animator Animator() => _animator;
}
