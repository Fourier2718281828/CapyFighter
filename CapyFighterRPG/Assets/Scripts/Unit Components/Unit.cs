using UnityEngine;

public class Unit : MonoBehaviour
{
    #region Fields
    private UnitData _data;
    private Animator _animator;
    private Fighter _fighter;
    private AudioSource _audioSource;
    private Transform _transform;

    [SerializeField] private GameObject _damageCanvas;
    #endregion

    #region MonoBehaviour Methods
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _fighter = GetComponent<Fighter>();
        _audioSource = GetComponent<AudioSource>();
        _transform = GetComponent<Transform>();
    }

    private void OnEnable()
    {
        _fighter.OnAttacked += _ =>
        {
            _animator.Play("Attack");
            _audioSource.clip = _data.AttackSound;
            _audioSource.Play();
        };

        _fighter.OnSuperAttacked += _ =>
        {
            _animator.Play("SuperAttack");
            _audioSource.clip = _data.SuperAttackSound;
            _audioSource.Play();
        };

        _fighter.OnShieldEquiped += () =>
        {
            _animator.Play("ShieldEquipping");
            _animator.SetBool("IsShielded", true);
            _audioSource.clip = _data.ShieldEquipedSound;
            _audioSource.Play();
        };

        _fighter.OnShieldBroken += () =>
        {
            _animator.Play("ShieldBreaking");
            _animator.SetBool("IsShielded", false);
            _audioSource.clip = _data.ShieldBrokenSound;
            _audioSource.Play();
        };

        _fighter.ShieldHurtAnimation += () =>
        {
            _animator.Play("ShieldHurt");
            _audioSource.clip = _data.ShieldHurtSound;
            _audioSource.Play();
        };

        _fighter.HurtAnimation += () =>
        {
            _animator.Play("Hurt");
            _audioSource.clip = _data.HurtSound;
            _audioSource.Play();
        };

        _fighter.OnDied += () =>
        {
            _audioSource.clip = _data.DieSound;
            _audioSource.Play();
        };
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
