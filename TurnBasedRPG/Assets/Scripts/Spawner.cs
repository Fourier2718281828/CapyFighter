using UnityEngine;
using System;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Unit[] _heroSlots;
    [SerializeField] private Unit[] _enemySlots;

    public void SpawnHeroAtSlot(int slot)
    {
        if (slot >= _heroSlots.Length)
            throw new Exception("");

        Instantiate(_heroSlots[slot].Placeholder);
    }

    public void SpawnEnemyAtSlot(int slot)
    {
        if (slot >= _enemySlots.Length)
            throw new Exception("");

        Instantiate(_enemySlots[slot].Placeholder);
    }
}
