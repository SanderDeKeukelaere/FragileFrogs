using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    float _intervalSpawnTime;
    float _currentSpawnTime;
    Vector3 _spawnLocation;

    List<Tuple<int,GameObject>> _enemies = new List<Tuple<int, GameObject>>();
    int _currentAmountLeft = 0;

    bool _isSpawning = false;
    public bool IsSpawning { get { return _isSpawning; } }

    public void StartWave(List<Tuple<int, GameObject>> enemies, float duration)
    {
        _isSpawning = true;
        _enemies = enemies;
        int amountOfEnemies = 0;
        foreach (Tuple<int, GameObject> enemy in enemies)
        {
            amountOfEnemies+= enemy.Item1;
        }
        _currentAmountLeft = amountOfEnemies;
        _intervalSpawnTime = duration / amountOfEnemies;
        _currentSpawnTime = _intervalSpawnTime;
    }
    private void Start()
    {
        _spawnLocation = FindObjectOfType<TileManager>().Path[0];
        Debug.Log(_spawnLocation);
        Debug.Log(FindObjectOfType<TileManager>().Path.Count);
    }
    private void Update()
    {
        if (_isSpawning == false)
        {
            return;
        }

        _currentSpawnTime -= Time.deltaTime;
        if (_currentSpawnTime < 0f)
        {
            HandleSpawn();
            _currentSpawnTime = _intervalSpawnTime;
            --_currentAmountLeft;
            if (_currentAmountLeft <= 0)
            {
                _isSpawning = false;
            }
        }
    }
    private void HandleSpawn()
    {
        int id = Random.Range(0, _enemies.Count);
        Tuple<int, GameObject> enemy = _enemies[id];
        Spawn(enemy.Item2);
        Tuple<int, GameObject> updated = new Tuple<int, GameObject>(enemy.Item1 - 1, enemy.Item2);
        _enemies[id] = updated;
        if (updated.Item1 <= 0)
        {
            _enemies.RemoveAt(id);
        }
    }
    private void Spawn(GameObject enemy)
    {
        Instantiate(enemy, _spawnLocation, Quaternion.identity);
    }
}