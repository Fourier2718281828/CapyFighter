using UnityEngine;

public class GameProgress : MonoBehaviour
{
    private CombatController _combatController;
    public bool IsInitialized { get; private set; }

    #region Fields to Serialize
    public int Level { get; private set; }
    public int LevelProgress { get; private set; }
    #endregion

    #region Main Statistics
    public int TotalDamage { get; private set; }

    #endregion

    private void Awake()
    {
        _combatController = GetComponent<CombatController>();
    }

    private void Start()
    {
        AssignEventsToFighters();
    }

    private void Update()
    {
        Debug.Log($"Level = {Level}, EXP = {LevelProgress}");
    }

    public void Init(GameProgressSave save)
    {
        Level = save.Level;
        LevelProgress = save.LevelProgress;
        IsInitialized = true;
        TotalDamage = 0;
    }

    private void AssignEventsToFighters()
    {
        foreach (var enemyFighter in _combatController.EnemiesToFighters.Values)
        {
            enemyFighter.OnTotalDamageReceived += (damage) =>
            {
                TotalDamage += damage;
                GetExperience(damage);
                UpdateAllAchievements();
            };

            enemyFighter.OnTotalShieldDamageReceived += (damage) =>
            {
                TotalDamage += damage;
                GetExperience(damage);
                UpdateAllAchievements();
            };
        }
    }

    private void UpdateAllAchievements()
    {
        //foreach(var ach in Achievements)
        //{
        //    ach.UpdateProgress();
        //}

        //Achievements.UpdateProgress();
    }

    public void GetExperience(int exp)
    {
        ++Level;
        LevelProgress += exp;
    }
}