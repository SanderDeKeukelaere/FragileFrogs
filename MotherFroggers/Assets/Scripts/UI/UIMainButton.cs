using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIMainButton : MonoBehaviour
{
    GameManager _gameManager;
    ItemManager _itemManager;
    [SerializeField] TextMeshProUGUI _buttonText;
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
        if (_gameManager.IsInWave == false && _gameManager.RemainingItemsToPlace > 0)
        {
            _buttonText.text = "Confirm";
        }
        else if (_gameManager.IsInWave == false)
        {
            _buttonText.text = "Start";
        }
    }
}
