using System.Collections.Generic;

[System.Serializable]
public class GameProgressSave
{
    public int Level;
    public int LevelProgress;

    public List<Achievement> Achievements;
    public GameStats Stats;

    public GameProgressSave(GameProgress progress)
    {
        Level = progress.Level;
        LevelProgress = progress.LevelProgress;
        Achievements = progress.Achievements;
        Stats = progress.GameStats;

        //foreach (var ach in Achievements)
        //{
        //    ach.Attach(Stats);
        //}
    }

    public GameProgressSave(List<Achievement> achievements)
    {
        Level = 0;
        LevelProgress = 0;
        Achievements = achievements;
        Stats = new GameStats();

        //foreach(var ach in Achievements)
        //{
        //    ach.Attach(Stats);
        //}
    }
}