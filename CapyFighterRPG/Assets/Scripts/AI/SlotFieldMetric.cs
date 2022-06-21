using UnityEngine;

public class SlotFieldMetric : MonoBehaviour
{
    /// <summary>
    /// The field, which heros and enemies
    /// are located at has a parallelogram shape.
    /// To calculate game distance we have to 
    /// switch to rectangular coordinates.
    /// _transform is the change of basis matrix.
    /// </summary>
    private Matrix2x2 _transform;
    private Vector3[] _heroPositions; 

    private void Start()
    {
        var spawner = GetComponent<Spawner>();
    }

    /// <summary>
    /// Function of game distance between slots.
    /// Distance between slots, lying in the same
    /// row and in 1st column, is 1f.
    /// Other distances are calculated according
    /// to euclidian metric. Distance between
    /// neighbouring slots is 1f.
    /// </summary>
    /// <param name="i1">first slot row</param>
    /// <param name="j1">first slot col</param>
    /// <param name="i2">second slot row</param>
    /// <param name="j2">second slot col</param>
    /// <returns></returns>
    public float SlotMetric(int i1, int j1, int i2, int j2)
    {
       
        return 0f;
    }
}
