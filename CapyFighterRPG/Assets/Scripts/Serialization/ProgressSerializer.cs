using UnityEngine;
using System.Collections.Generic;

public class ProgressSerializer : MonoBehaviour
{
    private GameProgress _currentProgress;
    private GameStats _currentStats;

    [SerializeField] private List<AchievementData> _achievements;

    private void Awake()
    {
        _currentProgress = GetComponent<GameProgress>();
        _currentStats = _currentProgress.GameStats;
        LoadProgress();
    }

    public void SaveProgress()
    {
        Serializer.Serialize(_currentProgress);
    }

    private void LoadProgress()
    {
        GameProgressSave save = Serializer.Deserialize();
        if (save == null)
        {
            //TODO make to do this when "new game" button is hit!!!
            var listOfAchievements = new List<Achievement>();
            foreach(var data in _achievements)
            {
                listOfAchievements.Add(new Achievement(data, null));
            }
            _currentProgress.Init(new GameProgressSave(listOfAchievements));
        }
        else
            _currentProgress.Init(save);
    }
}