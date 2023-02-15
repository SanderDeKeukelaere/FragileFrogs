using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Motherfrogger : MonoBehaviour
{
    [SerializeField] float _maxHp = 10f;
    float _currentHp;
    public float CurrentHp { get { return _currentHp; } }

    private void Awake()
    {
        _currentHp = _maxHp;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.ToLower() != "enemy")
        {
            return;
        }
        _currentHp -= other.gameObject.GetComponent<BasicEnemy>().Damage;
        Destroy(other.gameObject);
    }
}