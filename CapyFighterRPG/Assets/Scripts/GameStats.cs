[System.Serializable]
public class GameStats
{
    public int _totalDamage;

    public GameStats()
    {
        _totalDamage = 0;
    }

    public int TotalDamage
    {
        get => _totalDamage;
        set
        {
            if(value < 0) _totalDamage = 0;
            else _totalDamage = value;
        }
    }

}
