using UnityEngine;
using System;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject _unitPrefab;

    [Header("GameObject to store all units in. Not a unit prefab!")]
    [SerializeField] private GameObject _unitContainer;

    [Header("Slot Count Boundaries")]
    [Tooltip("Max number of hero slots in one column")]
    [SerializeField] private int _maxHeroRowCount;
    [Tooltip("Max number of enemy slots in one column")]
    [SerializeField] private int _maxEnemyRowCount;

    [Header("Boundary Points For the Field of Hero Slots :")]
    [SerializeField] private Vector3 _heroOrigin;
    [SerializeField] private Vector3 _heroOriginsNeighbour;
    [SerializeField] private Vector3 _heroDiagonalToOrigin;

    [Header("Boundary Points For the Field of Enemy Slots :")]
    [SerializeField] private Vector3 _enemyOrigin;
    [SerializeField] private Vector3 _enemyOriginsNeighbour;
    [SerializeField] private Vector3 _enemyDiagonalToOrigin;

    [Header("Heros at slots (not all slots have to be filled)")]
    [SerializeField] private UnitData[] _heroSlots;

    [Header("Enemies at slots (not all slots have to be filled)")]
    [SerializeField] private UnitData[] _enemySlots;

    private Vector3[] _heroSlotPositions;
    private Vector3[] _enemySlotPositions;

    public int MaxHeroRowCount => _maxHeroRowCount;
    public int MaxEnemyRowCount => _maxEnemyRowCount;
    public int MaxHeroColumnCount => _heroSlots.Length / _maxHeroRowCount;
    public int MaxEnemyColumnCount => _enemySlots.Length / _maxEnemyRowCount;
    public UnitData[] HeroSlots => _heroSlots;
    public UnitData[] EnemySlots => _enemySlots;
    public Vector3[] HeroSlotPositions => _heroSlotPositions;
    public Vector3[] EnemySlotPositions => _enemySlotPositions;


    public void Awake()
    {
        EvaluateSlotPositions();
    }

    public GameObject SpawnHeroAtSlot(int slot)
    {
        if (slot >= HeroSlots.Length)
            throw new IndexOutOfRangeException($"{slot}");
        if (HeroSlots[slot] == null)
            throw new InvalidOperationException($"Trying to instantiate a hero at an empty slot : {slot}");

        GameObject spawned = 
            Instantiate(_unitPrefab, _heroSlotPositions[slot], Quaternion.identity, _unitContainer.transform);

        spawned.GetComponent<Unit>().Init(HeroSlots[slot]);
        spawned.GetComponent<SpriteRenderer>().flipX = false;

        return spawned;
    }

    public GameObject SpawnEnemyAtSlot(int slot)
    {
        if (slot >= EnemySlots.Length)
            throw new IndexOutOfRangeException($"{slot}");
        if (EnemySlots[slot] == null)
            throw new InvalidOperationException($"Trying to instantiate an enemy at an empty slot : {slot}");

        GameObject spawned =
            Instantiate(_unitPrefab, _enemySlotPositions[slot], Quaternion.identity, _unitContainer.transform);

        spawned.GetComponent<Unit>().Init(EnemySlots[slot]);
        spawned.GetComponent<SpriteRenderer>().flipX = true;

        return spawned;
    }

    private void EvaluateSlotPositions()
    {
        VectorGrid grid;

        grid = new VectorGrid
            (
            _heroOrigin,
            _heroOriginsNeighbour,
            _heroDiagonalToOrigin,
            GetSlotRowsCount(HeroSlots.Length, MaxHeroRowCount),
            GetSlotColsCount(HeroSlots.Length, MaxHeroRowCount)
            );

        _heroSlotPositions = grid.CalculateCellCentres();

        grid = new VectorGrid
            (
            _enemyOrigin,
            _enemyOriginsNeighbour,
            _enemyDiagonalToOrigin,
            GetSlotRowsCount(EnemySlots.Length, MaxEnemyRowCount),
            GetSlotColsCount(EnemySlots.Length, MaxEnemyRowCount)
            );

        _enemySlotPositions = grid.CalculateCellCentres();

    }

    private int GetSlotRowsCount(int totalSlotCount, int maxRowCount)
    {
        if (totalSlotCount < maxRowCount)
            return totalSlotCount;
        else
            return maxRowCount;
    }
    private int GetSlotColsCount(int totalSlotCount, int maxRowCount)
    {
        if (totalSlotCount < maxRowCount)
            return 1;
        else
        {
            if (totalSlotCount % maxRowCount != 0)
                throw new InvalidOperationException
                    ("Max number of rows must devide the total number of slots provided " +
                    "at least one column is filled.");
            return totalSlotCount / maxRowCount;
        }
    }
}