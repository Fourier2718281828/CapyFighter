[System.Serializable]
public class GameProgressSave
{
    public int Level;
    public int LevelProgress;

    public GameProgressSave(GameProgress progress)
    {
        Level = progress.Level;
        LevelProgress = progress.LevelProgress;
    }

    public GameProgressSave()
    {
        Level = 0;
        LevelProgress = 0;
    }
}