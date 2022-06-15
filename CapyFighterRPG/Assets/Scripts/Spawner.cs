using UnityEngine;
using System;
    
public class Spawner : MonoBehaviour
{
    private CombatController _gameController;

    [SerializeField] private Transform[] _heroSlotPositions;  //Should there be Transform-s?
    [SerializeField] private Transform[] _enemySlotPositions; //Vector2?


    public void Awake()
    {
        _gameController = GetComponent<CombatController>();
        _heroSlotPositions = new Transform[_gameController.HeroSlots.Length];
        _enemySlotPositions = new Transform[_gameController.EnemySlots.Length];
        
        //TODO make arrays of transform get filled independently on how many heros and
        //enemies are in the scene
        for(int i = 0; i < _heroSlotPositions.Length; ++i)
        {
            _heroSlotPositions[i] = _gameController.HeroSlots[i]?.Placeholder.transform;
        }

        for (int i = 0; i < _enemySlotPositions.Length; ++i)
        {
            _enemySlotPositions[i] = _gameController.EnemySlots[i]?.Placeholder.transform;
        }
    }

    public GameObject SpawnHeroAtSlot(int slot)
    {
        if (slot >= _gameController.HeroSlots.Length)
            throw new IndexOutOfRangeException($"{slot}");
        if(_gameController.HeroSlots[slot] == null)
            throw new InvalidOperationException($"Trying to instantiate a hero at an empty slot : {slot}");

        _gameController.HeroSlots[slot].Placeholder.transform.position = _heroSlotPositions[slot].position;
        
        return Instantiate(_gameController.HeroSlots[slot].Placeholder);
    }

    public GameObject SpawnEnemyAtSlot(int slot)
    {
        if (slot >= _gameController.EnemySlots.Length)
            throw new IndexOutOfRangeException($"{slot}");
        if (_gameController.HeroSlots[slot] == null)
            throw new InvalidOperationException($"Trying to instantiate an enemy at an empty slot : {slot}");

        _gameController.EnemySlots[slot].Placeholder.transform.position = _enemySlotPositions[slot].position;

        return Instantiate(_gameController.EnemySlots[slot].Placeholder);
    }
}
