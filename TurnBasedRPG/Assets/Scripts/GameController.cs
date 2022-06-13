using UnityEngine;

public class GameController : MonoBehaviour
{
    private Spawner _spawner;
    private void Start()
    {
        _spawner = GetComponent<Spawner>();
        _spawner.SpawnHeroAtSlot(0);
        _spawner.SpawnHeroAtSlot(1);
        _spawner.SpawnEnemyAtSlot(0);
        _spawner.SpawnEnemyAtSlot(1);
    }
}
