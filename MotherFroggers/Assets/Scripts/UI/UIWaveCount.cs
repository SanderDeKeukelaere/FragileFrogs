using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIWaveCount : MonoBehaviour
{
    WaveManager _waveManager;
    [SerializeField] TextMeshProUGUI _waveText;

    private void Awake()
    {
        _waveManager = FindAnyObjectByType<WaveManager>();
        if (_waveManager == null)
        {
            Debug.LogError("WaveManager not found");
        }
    }
    void Update()
    {
        _waveText.text = $"{_waveManager.CurrentWave}/{_waveManager.MaxWave}";
    }
}
