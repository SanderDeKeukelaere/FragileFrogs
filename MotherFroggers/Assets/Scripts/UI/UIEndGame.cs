using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIEndGame : MonoBehaviour
{
    [SerializeField] GameObject _ui;
    [SerializeField] TextMeshProUGUI _title;
    [SerializeField] TextMeshProUGUI _time;
    [SerializeField] TextMeshProUGUI _score;

    Motherfrogger _motherfrogger;
    WaveManager _waveManager;
    GameManager _gameManager;
    private void Awake()
    {
        _motherfrogger = FindAnyObjectByType<Motherfrogger>();
        if (_motherfrogger == null)
        {
            Debug.LogError("Motherfrogger not found");
        }
        _waveManager = FindAnyObjectByType<WaveManager>();
        if (_waveManager == null)
        {
            Debug.LogError("WaveManager not found");
        }
        _gameManager = FindAnyObjectByType<GameManager>();
        if (_gameManager == null)
        {
            Debug.LogError("Gamemanager not found");
        }
        _ui.SetActive(false);
    }
    private void Update()
    {
        if (_motherfrogger.CurrentHp <= 0)
        {
            _ui.SetActive(true);
            _title.text = "GAME OVER";
        }
        else if (_waveManager.MaxWave == _waveManager.CurrentWave && _gameManager.IsInWave == false)
        {
            _ui.SetActive(true);
            _title.text = "GAME COMPLETED";
        }
        UpdateStats();
    }
    private void UpdateStats()
    {
        _time.text = (int)_gameManager.TotalTime + "";
        _score.text = (int)_gameManager.Score + "";
    }
}
