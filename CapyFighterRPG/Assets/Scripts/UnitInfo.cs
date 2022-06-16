using UnityEngine;

public class UnitInfo : MonoBehaviour
{
    [SerializeField] private GameObject _HPBarPrefab;
    [SerializeField] private GameObject _MPBarPrefab;

    private ProgressBar _HPBar;
    private ProgressBar _MPBar;

    private void Awake()
    {
        _HPBar = _HPBarPrefab.GetComponent<ProgressBar>();
        _MPBar = _MPBarPrefab.GetComponent<ProgressBar>();
    }

    public void SetHP(float amount)
    {
        _HPBar.SetProgress(amount);
    }

    public void SetMP(float amount)
    {
        _MPBar.SetProgress(amount);
    }
}