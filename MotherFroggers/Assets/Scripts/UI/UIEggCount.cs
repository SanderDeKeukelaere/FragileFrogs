using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIEggCount : MonoBehaviour
{
    GameManager _gameManager;
    WaveManager _waveManager;
    ItemManager _itemManager;
    [SerializeField] GameObject _ui;
    [SerializeField] TextMeshProUGUI _text;
    private void Awake()
    {
        _gameManager = FindAnyObjectByType<GameManager>();
        if (_gameManager == null)
        {
            Debug.LogError("Unable to find gamemanager");
        }
        _itemManager = FindAnyObjectByType<ItemManager>();
        if (_itemManager == null)
        {
            Debug.LogError("Unable to find itemmanager");
        }
        _waveManager = FindAnyObjectByType<WaveManager>();
        if (_waveManager == null)
        {
            Debug.LogError("Unable to find wavemanager");
        }
    }
    private void Update()
    {
        if (_waveManager.MaxWave == _waveManager.CurrentWave && _gameManager.IsInWave == false)
        {
            _ui.SetActive(false);
            return;
        }
        _text.text = $"{_gameManager.RemainingEggsToPlace}";
        _ui.SetActive(_itemManager.CurrentItem != null);
    }
}
