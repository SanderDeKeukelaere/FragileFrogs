using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpitProjectile : BaseProjectile
{
    private Vector3 _horizontalVelocity;
    private Vector3 _verticalVelocity;

    [SerializeField] private float _damage = 1.0f;

    [SerializeField] private float shootTime = 1.0f;

    private bool _isInit = false;

    private void Start()
    {
        Invoke("KillAfterTime", 5.0f);
    }

    private void KillAfterTime()
    {
        Destroy(gameObject);
    }

    protected override void UpdateProjectile()
    {
        if(!_isInit)
        {
            _isInit = true;

            _horizontalVelocity = (_target - transform.position) / shootTime;
            _horizontalVelocity.y = 0.0f;

            _verticalVelocity = (_target - transform.position - Physics.gravity * shootTime * shootTime) / shootTime;
            _verticalVelocity.x = 0.0f;
            _verticalVelocity.z = 0.0f;
            _verticalVelocity /= 2.0f;
        }

        _verticalVelocity += Physics.gravity * Time.deltaTime;
        transform.position += (_horizontalVelocity + _verticalVelocity) * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        BasicEnemy enemy = other.GetComponent<BasicEnemy>();
        if (enemy)
        {
            enemy.DoDamage(_damage);
            Destroy(gameObject);
        }
    }
}
