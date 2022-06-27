using System;

[Serializable]
public class Achievement
{
    private AchievementType _type;
    private string _name;

    private int _firstStarPoints;
    private int _secondStarPoints;
    private int _thirdStarPoints;

    private string _firstStarDescription;
    private string _secondStarDescription;
    private string _thirdStarDescription;

    private int _progress;

    private GameStats _gameStats;

    public Achievement(AchievementData data, GameStats stats)
    {
        _type = data.Type;
        _name = data.Name;

        _firstStarPoints = data.FirstStarPoints;
        _secondStarPoints = data.SecondStarPoints;
        _thirdStarPoints = data.ThirdStarPoints;

        _firstStarDescription = data.FirstStarDescription;
        _secondStarDescription = data.SecondStarDescription;
        _thirdStarDescription = data.ThirdStarDescription;

        _gameStats = stats;

        _progress = 0;
    }

    public void Attach(GameStats stats)
    {
        _gameStats = stats;
    }

    public bool IsFirstStarFulfilled()  => _progress >= _firstStarPoints;

    public bool IsSecondStarFulfilled() => _progress >= _secondStarPoints;

    public bool IsThirdStarFulfilled()  => _progress >= _thirdStarPoints;


    public void UpdateProgress()
    {
        switch(_type)
        {
            case AchievementType.Damage:
                if(_gameStats.TotalDamage >= _thirdStarPoints)
                    _progress = _thirdStarPoints;
                else 
                    _progress = _gameStats.TotalDamage;
                return;
            default:
                throw new InvalidOperationException("No such Achievement type");
        }
    }


    public AchievementType Type => _type;
    public string Name => _name;
    public int FirstStarPoints =>_firstStarPoints;
    public int SecondStarPoints => _secondStarPoints;
    public int ThirdStarPoints => _thirdStarPoints;

    public string FirstStarDescription => _firstStarDescription;
    public string SecondStarDescription => _secondStarDescription;
    public string ThirdStarDescription => _thirdStarDescription;

    public int Progress => _progress;
}