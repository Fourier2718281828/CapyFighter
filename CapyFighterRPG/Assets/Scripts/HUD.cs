using UnityEngine;
using System.Collections.Generic;
using System;

public class HUD : MonoBehaviour
{
    #region Fields
    [SerializeField] private GameObject _unitInfoPrefab;
    [SerializeField] private GameObject _hudCanvas;
    [Header("Field for hero infos")]
    [SerializeField] private Vector3 _heroOrigin;
    [SerializeField] private Vector3 _heroOriginsNeighbour;
    [SerializeField] private Vector3 _heroDiagonalToOrigin;
    [Header("Field for enemy infos")]
    [SerializeField] private Vector3 _enemyOrigin;
    [SerializeField] private Vector3 _enemyOriginsNeighbour;
    [SerializeField] private Vector3 _enemyDiagonalToOrigin;

    private CombatController _controller;
    private Dictionary<Fighter, UnitInfo> _fightersToUnitInfos;

    #endregion

    #region Properties
    public Dictionary<Fighter, UnitInfo> FightersToUnitInfos => _fightersToUnitInfos;
    #endregion

    #region MonoBehaviour methods
    private void Awake()
    {
        _controller = GetComponent<CombatController>();
        _fightersToUnitInfos = new Dictionary<Fighter, UnitInfo>();
    }

    private void SubscribeEvents()
    {
        foreach(var pair in FightersToUnitInfos)
        {
            pair.Key.OnDamageReceived += percentage => pair.Value.SetHP(percentage);
            //... for other attacks
        }
    }

    #endregion

    #region Custom Methods
    public void PlaceAllUnitInfos()
    {
        Vector3[] positions;
        GameObject unitInfoObject;
        UnitInfo script;

        positions = CalculateHeroInfosPositions();

        foreach (var unit in _controller.HerosToFighters)
        {
            unitInfoObject = Instantiate(_unitInfoPrefab, positions[_controller.GetHeroSlot(unit.Key)], 
                                        Quaternion.identity, _hudCanvas.transform);
            script = unitInfoObject.GetComponent<UnitInfo>();
            _fightersToUnitInfos.Add(unit.Value, script);
        }

        positions = CalculateEnemyInfosPositions();

        foreach (var unit in _controller.EnemiesToFighters)
        {
            unitInfoObject = Instantiate(_unitInfoPrefab, positions[_controller.GetEnemySlot(unit.Key)],
                                        Quaternion.identity, _hudCanvas.transform);
            script = unitInfoObject.GetComponent<UnitInfo>();
            _fightersToUnitInfos.Add(unit.Value, script);
        }

        SubscribeEvents();
    }

    private Vector3[] CalculateHeroInfosPositions()
    {
        const int rowsOfUnitInfoSlots = 1;

        VectorGrid grid = new VectorGrid
            (
            _heroOrigin,
            _heroOriginsNeighbour,
            _heroDiagonalToOrigin,
            _controller.HeroCount,
            rowsOfUnitInfoSlots
            );
        
        return grid.CalculateCellCentres();
    }

    private Vector3[] CalculateEnemyInfosPositions()
    {
        const int rowsOfUnitInfoSlots = 1;

        VectorGrid grid = new VectorGrid
            (
            _enemyOrigin,
            _enemyOriginsNeighbour,
            _enemyDiagonalToOrigin,
            _controller.EnemyCount,
            rowsOfUnitInfoSlots
            );

        return grid.CalculateCellCentres();
    }
    #endregion
}
