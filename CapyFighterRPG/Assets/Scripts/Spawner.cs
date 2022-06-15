using UnityEngine;
using System;

public class Spawner : MonoBehaviour
{
    //private Controller _combatController;

    [SerializeField] private GameObject DEBUG_POINT;

    [SerializeField] private GameObject _unitPrefab;
    //[SerializeField] private float _margin;
    [SerializeField] private int _maxSlotsInOneColumn;

    [Header("Boundary Points For the Field of Slots :")]
    [SerializeField] private Vector3 _leftLowBoundPoint;
    [SerializeField] private Vector3 _leftUpBoundPoint;
    [SerializeField] private Vector3 _rightUpBoundPoint;
    [SerializeField] private Vector3 _rightLowBoundPoint;

    [Header("Heros at slots (not all slots have to be filled)")]
    [SerializeField] private UnitData[] _heroSlots;

    [Header("Enemies at slots (not all slots have to be filled)")]
    [SerializeField] private UnitData[] _enemySlots;

    private Vector3[] _heroSlotPositions;
    private Vector3[] _enemySlotPositions;

    public int MaxSlotsInOneColumn => _maxSlotsInOneColumn;
    public UnitData[] HeroSlots => _heroSlots;
    public UnitData[] EnemySlots => _enemySlots;

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

        _unitPrefab.transform.position = _heroSlotPositions[slot];
        _unitPrefab.GetComponent<Unit>().Init(HeroSlots[slot]);
        _unitPrefab.GetComponent<SpriteRenderer>().flipX = false;

        return Instantiate(_unitPrefab);
    }

    public GameObject SpawnEnemyAtSlot(int slot)
    {
        if (slot >= EnemySlots.Length)
            throw new IndexOutOfRangeException($"{slot}");
        if (HeroSlots[slot] == null)
            throw new InvalidOperationException($"Trying to instantiate an enemy at an empty slot : {slot}");

        _unitPrefab.transform.position = _enemySlotPositions[slot];
        _unitPrefab.GetComponent<Unit>().Init(EnemySlots[slot]);
        _unitPrefab.GetComponent<SpriteRenderer>().flipX = true;

        return Instantiate(_unitPrefab);
    }

    private void EvaluateSlotPositions()
    {
        _heroSlotPositions = CalculateSlotCentresGrid
                        (3, 2, _leftLowBoundPoint, _leftUpBoundPoint,
                         _rightUpBoundPoint, _rightLowBoundPoint);

        var enemy_leftLowBoundPoint = _leftLowBoundPoint;
        var enemy_leftUpBoundPoint = _leftUpBoundPoint;
        var enemy_rightUpBoundPoint = _rightUpBoundPoint;
        var enemy_rightLowBoundPoint = _rightLowBoundPoint;
        enemy_leftLowBoundPoint.Scale(new Vector3(-1,1,1));
        enemy_leftUpBoundPoint.Scale(new Vector3(-1,1,1));
        enemy_rightUpBoundPoint.Scale(new Vector3(-1,1,1));
        enemy_rightLowBoundPoint.Scale(new Vector3(-1, 1, 1));

        _enemySlotPositions = CalculateSlotCentresGrid
                        (3, 2, enemy_leftLowBoundPoint, enemy_leftUpBoundPoint,
                         enemy_rightUpBoundPoint, enemy_rightLowBoundPoint);
    }

    private Vector3[] CalculateSlotCentresGrid(int cols, int rows, 
            Vector3 leftDown, Vector3 leftUp, Vector3 rightUp, Vector3 rightDown)
    {
        //d - diagonal vector of the paralelogram
        //d = d_e1 + d_e2
        //d_e1 = m*e1  =>  e1 = d_e1 / 2m
        //d_e2 = n*e2  =>  e2 = d_e2 / 2n
        //rightDown + (e1 + e2)

        var e1 = (leftDown - rightDown) / (2 * rows);
        var e2 = (rightUp - rightDown) / (2 * cols);
        var o = rightDown + (e1 + e2);

        var result = new Vector3[rows * cols];

        for(int i = 0; i < rows; ++i)
            for(int j = 0; j < cols; ++j)
            {
                result[i * cols + j] = o + 2*i*e1 + 2*j*e2;
                DRAW_DEBUG(result[i * cols + j]);
            }

        return result;
    }

    private int GetSlotRowsNumber() => 0;
    private int GetSlotColsNumber() => 0;


    private void DRAW_DEBUG(Vector3 position)
    {
        DEBUG_POINT.transform.position = position;
        Instantiate(DEBUG_POINT);
    }
}
