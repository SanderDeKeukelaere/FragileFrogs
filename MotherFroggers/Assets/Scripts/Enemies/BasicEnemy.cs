using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : MonoBehaviour
{

    private static List<BasicEnemy> _enemies = new List<BasicEnemy>();
    public static List<BasicEnemy> Enemies { get { return _enemies; } }

    private static List<Vector3> _path = null;
    private List<Vector3> _checkpoints = null;

    [SerializeField] float _speed = 1f;
    [SerializeField] float _damage = 1f;
    [SerializeField] float _health = 1f;
    [SerializeField] float _rotateSpeed = 6f;

    public float Damage
    {
        get { return _damage; }
    }

    /// <summary>
    /// Deals damage against the enemy and destroys him.
    /// </summary>
    /// <param name="damage"></param>
    /// <returns>Is dead (returns true if he died)</returns>
    public bool DoDamage(float damage)
    {
        _health -= damage;
        if (_health <= 0f)
        {
            Destroy(gameObject);
            return true;
        }
        return false;
    }

    private void Awake()
    {
        GetPath();
    }
    private void Start()
    {
        _enemies.Add(this);
    }
    private void Update()
    {
        if (_checkpoints == null)
        {
            GetPath();
        }
        UpdatePath();
    }
    private void OnDestroy()
    {
        _enemies.Remove(this);
    }
    private void GetPath()
    {
        if (_path == null)
        {
            TileManager tileManager = FindAnyObjectByType<TileManager>();
            if (tileManager == null)
            {
                Debug.LogError("Unable to find class TileManager");
                return;
            }
            _path = FindAnyObjectByType<TileManager>().Path;
            if (_path == null)
            {
                Debug.LogError("Path not generated yet!");
                return;
            }
        }
        _checkpoints = new List<Vector3>(_path);
        float y = transform.position.y;
        for (int i = 0; i < _checkpoints.Count; i++)
        {
            _checkpoints[i] = new Vector3(_checkpoints[i].x, y, _checkpoints[i].z);
        }
    }
    private void UpdatePath()
    {
        if (_checkpoints.Count > 0)
        {
            if ((_checkpoints[0] - new Vector3(transform.position.x, transform.position.y, transform.position.z)).sqrMagnitude < 0.0001f)
            {
                _checkpoints.RemoveAt(0);
            }
            if (_checkpoints.Count == 0)
            {
                Debug.LogError("Yoink hp @ end");
                return;
            }
            Vector3 p = Vector3.MoveTowards(transform.position, _checkpoints[0], _speed);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(_checkpoints[0] - transform.position), Time.deltaTime * _rotateSpeed);
            GetComponent<Rigidbody>().MovePosition(p);
        }
        else
        {
            Debug.LogError("Yoink hp @ end");
        }
    }
}