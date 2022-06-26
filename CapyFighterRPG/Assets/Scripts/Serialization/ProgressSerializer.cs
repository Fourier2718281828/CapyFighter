using UnityEngine;

public class ProgressSerializer : MonoBehaviour
{
    private GameProgress _currentProgress;

    private void Awake()
    {
        _currentProgress = GetComponent<GameProgress>();
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
            _currentProgress.Init(new GameProgressSave());
        }
        else
            _currentProgress.Init(save);
    }
}