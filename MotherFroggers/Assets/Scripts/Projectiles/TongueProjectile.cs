using UnityEngine;

public class TongueProjectile : BaseProjectile
{
    private bool _hasHitSomething = false;
    private bool _hasLicked = false;
    [SerializeField] private float _maxTongueScale = 1.0f;
    [SerializeField] private float _tongueSpeed = 1.0f;
    [SerializeField] private float _damage = 1.0f;

    protected override void UpdateProjectile()
    {
        Vector3 scale = transform.localScale;
        if (!_hasLicked)
        {
            scale.z += _tongueSpeed * Time.deltaTime;
            _hasLicked = scale.z > _maxTongueScale;
        }
        else
        {
            scale.z -= _tongueSpeed * Time.deltaTime;
            if (scale.z < 0) Destroy(gameObject);
        }
        transform.localScale = scale;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_hasHitSomething) return;

        BasicEnemy enemy = other.GetComponent<BasicEnemy>();
        if (enemy)
        {
            enemy.DoDamage(_damage);
            _hasHitSomething = true;
        }
    }
}
