using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private WaveManager _waveManager = null;

    private AudioSource _audioBuildSource = null;
    private AudioSource _audioWaveSource = null;

    [SerializeField] private float _fadeSpeed = 0.5f;

    [SerializeField] private GameObject _startButton = null;

    [SerializeField] private AudioClip _buildMusic = null;
    [SerializeField] private AudioClip _waveMusic = null;

    bool _isInWave = true;

    private Motherfrogger _motherfrogger;

    private void Start()
    {
        _motherfrogger = FindAnyObjectByType<Motherfrogger>();
        _waveManager = GetComponent<WaveManager>();
        var audioSources = GetComponents<AudioSource>();
        _audioBuildSource = audioSources[0];
        _audioWaveSource = audioSources[1];

        _audioBuildSource.clip = _buildMusic;
        _audioWaveSource.clip = _waveMusic;
    }

    public void NextWave()
    {
        _waveManager.StartWave();

        _startButton.SetActive(false);

        _audioWaveSource.Play();

        _isInWave = true;
    }

    private void Update()
    {
        if(_isInWave && _waveManager.HasWaveEnded() && BasicEnemy.Enemies.Count == 0 && _motherfrogger.CurrentHp > 0)
        {
            _startButton.SetActive(true);

            _audioBuildSource.Play();

            _isInWave = false;
        }

        if(_isInWave)
        {
            _audioBuildSource.volume -= _fadeSpeed * Time.deltaTime;
            if(_audioBuildSource.volume < 0)
            {
                _audioBuildSource.volume = 0.0f;
                _audioBuildSource.Stop();
            }

            _audioWaveSource.volume += _fadeSpeed * Time.deltaTime;
            if (_audioWaveSource.volume > 1.0f) _audioWaveSource.volume = 1.0f;
        }
        else
        {
            _audioWaveSource.volume -= _fadeSpeed * Time.deltaTime;
            if (_audioWaveSource.volume < 0)
            {
                _audioWaveSource.volume = 0.0f;
                _audioWaveSource.Stop();
            }

            _audioBuildSource.volume += _fadeSpeed * Time.deltaTime;
            if (_audioBuildSource.volume > 1.0f) _audioBuildSource.volume = 1.0f;
        }
    }
}
