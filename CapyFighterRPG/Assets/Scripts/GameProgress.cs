using UnityEngine;
using System.Collections.Generic;

public class GameProgress : MonoBehaviour
{
    private CombatController _combatController;
    public bool IsInitialized { get; private set; }

    #region Fields to Serialize
    public int Level { get; private set; }
    public int LevelProgress { get; private set; }
    public List<Achievement> Achievements { get; private set; }
    #endregion

    #region Main Statistics
    public GameStats GameStats { get; private set; }

    #endregion

    private void Awake()
    {
        _combatController = GetComponent<CombatController>();
        IsInitialized = true;
        //GameStats = new GameStats();
    }

    private void Start()
    {
        AssignEventsToFighters();
    }

    private void Update()
    {
        //Debug.Log($"Size = {Achievements.Count}");
        Debug.Log($"first = {Achievements[0].IsFirstStarFulfilled()}, " +
            $"third = {Achievements[0].IsThirdStarFulfilled()}, damage = {GameStats.TotalDamage}");
    }

    public void Init(GameProgressSave save)
    {
        Level = save.Level;
        LevelProgress = save.LevelProgress;
        Achievements = save.Achievements;
        GameStats = save.Stats;

        foreach (var ach in Achievements)
        {
            ach.Attach(GameStats);
        }
    }

    private void AssignEventsToFighters()
    {
        foreach (var enemyFighter in _combatController.EnemiesToFighters.Values)
        {
            enemyFighter.OnTotalDamageReceived += (damage) =>
            {
                GameStats.TotalDamage += damage;
                GetExperience(damage);
                UpdateAllAchievements();
            };

            enemyFighter.OnTotalShieldDamageReceived += (damage) =>
            {
                GameStats.TotalDamage += damage;
                GetExperience(damage);
                UpdateAllAchievements();
            };
        }
    }

    private void UpdateAllAchievements()
    {
        foreach (var ach in Achievements)
        {
            ach.UpdateProgress();
        }
    }

    public void GetExperience(int exp)
    {
        ++Level;
        LevelProgress += exp;
    }
}