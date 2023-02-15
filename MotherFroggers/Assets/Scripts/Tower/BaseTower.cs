using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BaseTower : MonoBehaviour
{
    [SerializeField] private float _fireDelay = 1f; //Amount of seconds between firing
    [SerializeField] private float _damage = 1f;    
    [SerializeField] private float _range = 1f;
    private List<GameObject> _enemies = new List<GameObject>();
    private float _attackTimer = 0f;
    [SerializeField] private GameObject _projectilePrefab = null;
    [SerializeField] private Transform _projectileSocket = null;
    [SerializeField] private Transform _frog = null;

    private const string ENEMY_TAG = "Enemy";

    // Start is called before the first frame update
    void Start()
    {
        var capsule = GetComponentInChildren<CapsuleCollider>();
        capsule.radius = _range;
        capsule.height = 5 * capsule.radius;
    }

    // Update is called once per frame
    void Update()
    {
        //Check if enough time has passed since last attack
        if (_attackTimer <= 0f)
        {
            EnemyCleanup();
            Attack();
        }

        LookAtEnemy();

        //Update attack timer
        if (_attackTimer > 0f)
        {
            _attackTimer -= Time.deltaTime;
        }
    }

    void LookAtEnemy()
    {
        //Don't attack if no enemies are in range
        if (_enemies.Count == 0) return;

        //Get enemy closest to this tower
        GameObject closestEnemy = _enemies[0];
        if (!closestEnemy) return;

        foreach (GameObject enemy in _enemies)
        {
            if (!enemy) continue;

            if (Vector3.Distance(transform.position, enemy.transform.position) < Vector3.Distance(transform.position, closestEnemy.transform.position))
            {
                closestEnemy = enemy;
            }
        }

        if (!closestEnemy) return;

        _frog.LookAt(closestEnemy.transform);
        Vector3 rotation = _frog.eulerAngles;
        rotation.x = 0.0f;
        rotation.z = 0.0f;
        _frog.eulerAngles = rotation;
    }

    private void EnemyCleanup()
    {
        //Check if there are any enemies
        if (_enemies.Count == 0) return;

        //Enemy cleanup
        for (int i = _enemies.Count - 1; i >= 0; i--)
        {
            //Check if enemy has died
            if (_enemies[i] == null)
            {
                _enemies.RemoveAt(i);
            }
        }
    }

    private void Attack()
    {
        if (!_projectilePrefab) return;

        //Don't attack if no enemies are in range
        if (_enemies.Count == 0) return;

        //Get enemy closest to this tower
        GameObject closestEnemy = _enemies[0];
        foreach (GameObject enemy in _enemies)
        {
            if (Vector3.Distance(transform.position, enemy.transform.position) < Vector3.Distance(transform.position, closestEnemy.transform.position))
            {
                closestEnemy = enemy;
            }
        }

        //Attack closest enemy
        GameObject projectileClone = Instantiate(_projectilePrefab, _projectileSocket.position, _frog.rotation, _frog);
        projectileClone.GetComponent<BaseProjectile>().Init(closestEnemy.transform.position);

        _attackTimer = _fireDelay;
    }

    private void OnTriggerEnter(Collider other)
    {
        //return if null or already in the list
        if (other == null) return;
        if (_enemies.Contains(other.gameObject)) return;

        //add object to enemy list if it has enemy tag
        if (other.CompareTag(ENEMY_TAG))
        {
            _enemies.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //return if null
        if (other == null) return;

        //remove object from enemy list if present
        if (_enemies.Contains(other.gameObject))
        {
            _enemies.Remove(other.gameObject);
        }
    }
}
