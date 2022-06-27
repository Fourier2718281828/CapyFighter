using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementDepictor : MonoBehaviour
{
    [SerializeField] private GameObject _achievementPrefab;

    private RectTransform _canvasTransform;
    private const int _NumOfRowsAtPage = 3;
    private Vector3[] _positions;

    private void Awake()
    {
        _canvasTransform = GetComponent<RectTransform>();
        _positions = new Vector3[_NumOfRowsAtPage];
        EvaluatePositions();
        SpawnAchievements();
    }

    private void EvaluatePositions()
    {
        var totalHeight = _canvasTransform.rect.height;
        var rowHeight = totalHeight / _NumOfRowsAtPage;
        var halfRowHeight = rowHeight / 2;
        var firstRowTop = totalHeight / 2;

        for (int i = 0; i < _NumOfRowsAtPage; i++)
        {
            _positions[i] = new Vector3(0f, firstRowTop - halfRowHeight - i * rowHeight, 1f);
            Debug.Log(_positions[i].ToString());
        }
    }

    private void SpawnAchievements()
    {
        foreach(var position in _positions)
        {
            GameObject ach = Instantiate(_achievementPrefab, _canvasTransform);
            var transform = (ach.transform) as RectTransform;
            transform.anchoredPosition = position;
        }
    }
}
