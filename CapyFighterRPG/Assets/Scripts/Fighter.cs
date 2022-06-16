using UnityEngine;

public class Fighter : MonoBehaviour
{
    private Unit _unit;
    private UnitInfo _unitInfo;
    //private CombatController _combatController;

    private void Awake()
    {
        _unit = GetComponent<Unit>();
        //_combatController = combatController.GetComponent<CombatController>();
    }

    private void Start()
    {
        GameObject combatController = GameObject.FindGameObjectWithTag("CombatController");
        HUD hud = combatController.GetComponent<HUD>();
        _unitInfo = hud.UnitInfos[_unit];
    }

    public void ReceiveDamage(float damage)
    {
        _unitInfo.SetHP(0.5f);
    }

    public void Attack(Unit victim)
    {
        _unitInfo.SetMP(0.5f);
        victim.Fighter.ReceiveDamage(_unit.Damage);
    }
}