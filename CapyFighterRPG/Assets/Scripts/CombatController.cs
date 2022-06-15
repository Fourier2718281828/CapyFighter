using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;

public class CombatController : StateMachine
{
    #region Fields
    private Spawner _spawner;
    private Dictionary<int, GameObject> _spawnedHeros;
    private Dictionary<int, GameObject> _spawnedEnemies;
    private Animator[] _heroAnimators;
    private Animator[] _enemyAnimators;

    [SerializeField] private Unit[] _heroSlots;
    [SerializeField] private Unit[] _enemySlots;
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private int _selectedHeroSlot;
    [SerializeField] private int _selectedEnemySlot;

    [HideInInspector] public EnemyTurnState EnemyTurn;
    [HideInInspector] public HeroTurnState HeroTurn;
    [HideInInspector] public PauseState Pause;

    //[HideInInspector] public PausableState Pausable;

    #endregion

    #region Properties
    public Unit[] HeroSlots => _heroSlots;
    public Unit[] EnemySlots => _enemySlots;
    public Dictionary<int, GameObject> SpawnedHeros => _spawnedHeros;
    public Dictionary<int, GameObject> SpawnedEnemies => _spawnedEnemies;
    public Animator[] HeroAnimators => _heroAnimators;
    public Animator[] EnemyAnimators => _enemyAnimators;
    public int SelectedHeroSlot 
    { 
        get => _selectedHeroSlot;
        private set => _selectedHeroSlot = value;
    }
    public int SelectedEnemySlot
    {
        get => _selectedEnemySlot;
        private set => _selectedEnemySlot = value;
    }
    #endregion

    #region MonoBehaviour Methods
    private void Awake()
    {
        _spawner = GetComponent<Spawner>();
        _spawnedHeros = new Dictionary<int, GameObject>();
        _spawnedEnemies = new Dictionary<int, GameObject>();
        _heroAnimators = new Animator[HeroSlots.Length];
        _enemyAnimators = new Animator[EnemySlots.Length];

        EnemyTurn = new EnemyTurnState(this);
        HeroTurn = new HeroTurnState(this);
        Pause = new PauseState(this);

        SelectedHeroSlot = SelectedEnemySlot = -1;
    }

    private void Start()
    {
        base.SetUp();

        GameObject spawnedObject;

        for (int i = 0; i < _heroSlots.Length; ++i)
        {
            if (_heroSlots[i] != null)
            {
                spawnedObject = _spawner.SpawnHeroAtSlot(i);
                SpawnedHeros.Add(i, spawnedObject);
                _heroAnimators[i] = spawnedObject.GetComponent<Animator>();
            }
        }

        for (int i = 0; i < _enemySlots.Length; ++i)
        {
            if (_enemySlots[i] != null)
            {
                spawnedObject = _spawner.SpawnEnemyAtSlot(i);
                SpawnedEnemies.Add(i, spawnedObject);
                _enemyAnimators[i] = spawnedObject.GetComponent<Animator>();
            }
        }
    }

    private void Update()
    {
        base.UpdateLogic();
        OnMouseClicked();
    }

    #endregion

    #region CustomMethods
    //But try to avoid using these. Act in terms of slots, not prefabs
    public int GetHeroSlot(GameObject hero)
    {
        try
        {
            var foundPair = SpawnedHeros.First(x => x.Value.Equals(hero));
            return foundPair.Key;
        }
        catch (InvalidOperationException)
        {
            throw new InvalidOperationException("Trying to get slot of non-existent hero");
        }
    }

    public int GetEnemySlot(GameObject enemy)
    {
        try
        {
            var foundPair = SpawnedEnemies.FirstOrDefault(x => x.Value.Equals(enemy));
            return foundPair.Key;
        }
        catch (InvalidOperationException)
        {
            throw new InvalidOperationException("Trying to get slot of non-existent hero");
        }
    }

    private void OnMouseClicked()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SetSelectedUnitsSlots();
            //if(SelectedHeroSlot != -1)
            //    _heroAnimators[SelectedHeroSlot].Play(_heroSlots[SelectedHeroSlot].AttackAnimationName);
        }
    }

    private void SetSelectedUnitsSlots()
    {
        Collider2D detectedCollider = GetColliderIntersectingMouse();

        if (detectedCollider == null)
        {
            RefreshSelected();
            return;
        }
        
        try
        {
            SelectedHeroSlot = GetHeroSlot(detectedCollider.gameObject);
        }
        catch(InvalidOperationException)
        {
            SelectedEnemySlot = GetEnemySlot(detectedCollider.gameObject);
        }
    }

    private Collider2D GetColliderIntersectingMouse()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = _mainCamera.nearClipPlane;
        Vector2 mouseWorldPosition = _mainCamera.ScreenToWorldPoint(mousePosition);

        return Physics2D.OverlapBox(mouseWorldPosition, new Vector2(0.2f, 0.2f), 0);
    }

    public void RefreshSelected()
    {
        SelectedHeroSlot = SelectedEnemySlot = - 1;
    }

    protected override State InitialState()
    {
        return HeroTurn;
    }
    #endregion
}
