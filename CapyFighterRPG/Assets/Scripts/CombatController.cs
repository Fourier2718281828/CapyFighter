using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;

public class CombatController : StateMachine
{
    #region Fields
    private Spawner _spawner;
    private Dictionary<GameObject, int> _herosToSlots;
    private Dictionary<GameObject, int> _enemiesToSlots;
    private Dictionary<GameObject, Unit> _herosToUnits;
    private Dictionary<GameObject, Unit> _enemiesToUnits;
    private int _selectedHeroSlot;
    private int _selectedEnemySlot;

    [SerializeField] private Camera _mainCamera;

    [HideInInspector] public EnemyTurnState EnemyTurn;
    [HideInInspector] public HeroTurnState HeroTurn;
    [HideInInspector] public PauseState Pause;

    #endregion

    #region Properties
    public Dictionary<GameObject, int> HerosToSlots => _herosToSlots;
    public Dictionary<GameObject, int> EnemiesToSlots => _enemiesToSlots;
    public Dictionary<GameObject, Unit> HerosToUnits => _herosToUnits;
    public Dictionary<GameObject, Unit> EnemiesToUnits => _enemiesToUnits;
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
        _herosToSlots = new Dictionary<GameObject, int>();
        _enemiesToSlots = new Dictionary<GameObject, int>();
        _herosToUnits = new Dictionary<GameObject, Unit>();
        _enemiesToUnits = new Dictionary<GameObject, Unit>();

        EnemyTurn = new EnemyTurnState(this);
        HeroTurn = new HeroTurnState(this);
        Pause = new PauseState(this);

        SelectedHeroSlot = SelectedEnemySlot = -1;
    }

    private void Start()
    {
        if (!AreUnitArraysCorrect())
        {
            throw new InvalidProgramException("Unaccaptable number of slots");
        }

        base.SetUp();

        GameObject spawnedObject;

        for (int i = _spawner.HeroSlots.Length - 1; i >= 0 ; --i)
        {
            if (_spawner.HeroSlots[i] != null)
            {
                spawnedObject = _spawner.SpawnHeroAtSlot(i);
                HerosToSlots.Add(spawnedObject, i);
                HerosToUnits.Add(spawnedObject, spawnedObject.GetComponent<Unit>()) ;
            }
        }

        for (int i = _spawner.EnemySlots.Length - 1; i >= 0 ; --i)
        {
            if (_spawner.EnemySlots[i] != null)
            {
                spawnedObject = _spawner.SpawnEnemyAtSlot(i);
                EnemiesToSlots.Add(spawnedObject, i);
                EnemiesToUnits.Add(spawnedObject, spawnedObject.GetComponent<Unit>());
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

    private bool AreUnitArraysCorrect()
    {
        return  (_spawner.HeroSlots.Length < _spawner.MaxSlotsInOneColumn || 
                _spawner.HeroSlots.Length % _spawner.MaxSlotsInOneColumn == 0) &&
                (_spawner.EnemySlots.Length < _spawner.MaxSlotsInOneColumn ||
                _spawner.EnemySlots.Length % _spawner.MaxSlotsInOneColumn == 0);
    }

    public int GetHeroSlot(GameObject hero)
    {
        if(HerosToSlots.ContainsKey(hero))
        {
            return HerosToSlots[hero];
        }
        else
        {
            throw new InvalidOperationException("Trying to get slot of non-existent hero");
        }
    }

    public int GetEnemySlot(GameObject enemy)
    {
        if (EnemiesToSlots.ContainsKey(enemy))
        {
            return EnemiesToSlots[enemy];
        }
        else
        {
            throw new InvalidOperationException("Trying to get slot of non-existent enemy");
        }
    }

    private void OnMouseClicked()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SetSelectedUnitsSlots();
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
        catch (InvalidOperationException)
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
        SelectedHeroSlot = SelectedEnemySlot = -1;
    }

    protected override State InitialState()
    {
        return HeroTurn;
    }
    #endregion
}
