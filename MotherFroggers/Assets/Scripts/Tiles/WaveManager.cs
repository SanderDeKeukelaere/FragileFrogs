using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] List<Wave> _waves;

    int _currentWave = 0;

    public int CurrentWave { get { return _currentWave; } }
    public int MaxWave { get { return _waves.Count; } }

    Spawner _spawner;

    private void Start()
    {
        _spawner = GetComponent<Spawner>();
        if (_spawner == null)
        {
            Debug.LogError("Need spawner to function ()");
        }
    }
    private void Update()
    {
    }

    public bool HasWaveEnded()
    {
        return !_spawner.IsSpawning;
    }

    public void StartWave()
    {
        List<Tuple<int, GameObject>> wave = new List<Tuple<int, GameObject>>();
        foreach (WaveEnemies waveEnemy in _waves[_currentWave]._enemies)
        {
            wave.Add(new Tuple<int, GameObject>(waveEnemy._spawnAmount, waveEnemy._enemy));
        }
        _spawner.StartWave(wave, _waves[_currentWave]._duration);
        _currentWave++;
    }
}

[System.Serializable]
struct Wave
{
    public List<WaveEnemies> _enemies;
    public float _duration;
}

[System.Serializable]
struct WaveEnemies
{
    public GameObject _enemy;
    public int _spawnAmount;
}
