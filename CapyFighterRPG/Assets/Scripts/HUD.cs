using UnityEngine;
using System.Collections.Generic;

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
    private Dictionary<Unit, UnitInfo> _unitInfos;

    #endregion

    #region Properties
    public Dictionary<Unit, UnitInfo> UnitInfos => _unitInfos;
    #endregion

    #region MonoBehaviour methods
    private void Awake()
    {
        _controller = GetComponent<CombatController>();
        _unitInfos = new Dictionary<Unit, UnitInfo>();
    }
    #endregion

    #region Custom Methods
    public void PlaceAllUnitInfos()
    {
        Vector3[] positions;
        GameObject unitInfoObject;
        UnitInfo script;

        positions = CalculateHeroInfosPositions();

        foreach (var unit in _controller.HerosToUnits)
        {
            unitInfoObject = Instantiate(_unitInfoPrefab, positions[_controller.GetHeroSlot(unit.Key)], 
                                        Quaternion.identity, _hudCanvas.transform);
            script = unitInfoObject.GetComponent<UnitInfo>();
            _unitInfos.Add(unit.Value, script);
        }

        positions = CalculateEnemyInfosPositions();

        foreach (var unit in _controller.EnemiesToUnits)
        {
            unitInfoObject = Instantiate(_unitInfoPrefab, positions[_controller.GetEnemySlot(unit.Key)],
                                        Quaternion.identity, _hudCanvas.transform);
            script = unitInfoObject.GetComponent<UnitInfo>();
            _unitInfos.Add(unit.Value, script);
        }
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
