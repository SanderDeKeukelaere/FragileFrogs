using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIEndGame : MonoBehaviour
{
    [SerializeField] GameObject _ui;
    [SerializeField] TextMeshProUGUI _title;

    Motherfrogger _motherfrogger;
    private void Awake()
    {
        _motherfrogger = FindAnyObjectByType<Motherfrogger>();
        if (_motherfrogger == null)
        {
            Debug.LogError("Motherfrogger not found");
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
    }
}
