using UnityEngine;

public class GameProgress : MonoBehaviour
{
    private CombatController _combatController;
    public bool IsInitialized { get; private set; }

    public int Level { get; private set; }
    public int LevelProgress { get; private set; }

    private void Awake()
    {
        _combatController = GetComponent<CombatController>();
    }

    private void Start()
    {
        AssignEventsToFighters();
    }

    //private void Update() => Debug.Log($"Level = {Level}, EXP = {LevelProgress}");

    public void Init(GameProgressSave save)
    {
        Level = save.Level;
        LevelProgress = save.LevelProgress;
        IsInitialized = true;
    }

    private void AssignEventsToFighters()
    {
        foreach (var enemyFighter in _combatController.EnemiesToFighters.Values)
        {
            enemyFighter.OnTotalDamageReceived += (damage) => GetExperience(damage);
            enemyFighter.OnTotalShieldDamageReceived += (damage) => GetExperience(damage);
        }
    }

    public void GetExperience(int exp)
    {
        ++Level;
        LevelProgress += exp;
    }
}