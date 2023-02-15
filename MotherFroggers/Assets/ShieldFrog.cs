using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldFrog : BaseTower
{
    private void OnTriggerEnter(Collider other)
    {
        BasicEnemy enemy = other.GetComponent<BasicEnemy>();
        if (enemy)
        {
            enemy.DoDamage(100);
            Destroy(gameObject);
        }
    }
}
