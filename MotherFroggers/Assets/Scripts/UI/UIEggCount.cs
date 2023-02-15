using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIEggCount : MonoBehaviour
{
    GameManager _gameManager;
    [SerializeField] GameObject _ui;
    [SerializeField] TextMeshProUGUI _text;
    private void Awake()
    {
        _gameManager = FindAnyObjectByType<GameManager>();
        if (_gameManager == null)
        {
            Debug.LogError("Unable to find gamemanager");
        }
    }
    private void Update()
    {
        _text.text = $"";
    }
}
