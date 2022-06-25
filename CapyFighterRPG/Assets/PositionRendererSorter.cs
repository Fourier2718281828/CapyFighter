using UnityEngine;

public class PositionRendererSorter : MonoBehaviour
{
    [SerializeField] private int _sortingOrderOrigin;
    private Renderer _renderer;
    private Transform _transform;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>(); 
        _transform = transform;
    }

    // Update is called once per frame
    void Update()
    {
        _renderer.sortingOrder = (int)(_sortingOrderOrigin - _transform.position.y);
    }
}
