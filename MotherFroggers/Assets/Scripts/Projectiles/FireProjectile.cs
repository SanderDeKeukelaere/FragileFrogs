using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class FireProjectile : BaseProjectile
{
    static FireProjectile _instance = null;
    static GameManager _gameManager = null;

    [SerializeField] private float _damagePerSecond = 1f;

    [SerializeField] private float _duration = 3.0f;

    private bool _isInit = false;
    private void Awake()
    {
        if (_gameManager == null)
        {
            _gameManager = FindAnyObjectByType<GameManager>();
        }
    }

    private void Start()
    {
        if (_instance != null)
        {
            Destroy(_instance.gameObject);
        }
        _instance = this;
        Invoke("KillAfterTime", _duration);
    }
    private void Update()
    {
        if (_gameManager.IsInWave == false)
        {
            Destroy(gameObject);
        }
    }
    private void KillAfterTime()
    {
        Destroy(gameObject);
    }

    protected override void UpdateProjectile()
    {
        if (!_isInit)
        {
            _isInit = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        BasicEnemy enemy = other.GetComponent<BasicEnemy>();
        if (enemy)
        {
            enemy.DoDamage(_damagePerSecond * Time.deltaTime);
        }
    }
}
