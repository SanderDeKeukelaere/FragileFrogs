using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class BaseProjectile : MonoBehaviour
{
    protected Vector3 _target;

    public void Init(Vector3 target)
    {
        _target = target;
    }

    private void Update()
    {
        UpdateProjectile();
    }

    abstract protected void UpdateProjectile();
}
