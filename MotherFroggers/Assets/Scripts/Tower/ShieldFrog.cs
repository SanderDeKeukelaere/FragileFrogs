using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldFrog : BaseTower
{
    private float _health = 3;

    private void OnTriggerEnter(Collider other)
    {
        BasicEnemy enemy = other.GetComponent<BasicEnemy>();
        if (enemy)
        {
            enemy.DoDamage(100);
            _health -= enemy.Damage;
        }

        if(_health <= 0)
            Destroy(gameObject);
    }
}
