using UnityEngine;
using System.Collections.Generic;

public class GameStarter : MonoBehaviour
{
    [SerializeField] private List<AchievementData> _achievementDatas;
    public void SerializeNewGameProgress()
    {
        //var achievementsDatas = GetComponent<AchievementDataList>().Achievements;
        var listOfAchievements = new List<Achievement>();
        foreach (var data in _achievementDatas)
        {
            listOfAchievements.Add(new Achievement(data, null));
        }
        //_currentProgress.Init(new GameProgressSave(listOfAchievements));
        //SaveProgress();
        Serializer.Serialize(new GameProgressSave(listOfAchievements));
    }

    public bool SavedProgressExists() => Serializer.SavedProgressExists();
}
