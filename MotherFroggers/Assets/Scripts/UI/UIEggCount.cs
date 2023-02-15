using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIEggCount : MonoBehaviour
{
    GameManager _gameManager;
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
    }
    private void Update()
    {
        _text.text = $"{_gameManager.EggsToPlace}/{_gameManager.EggsPerWave}";
        _ui.SetActive(!_gameManager.IsInWave);
    }
}
