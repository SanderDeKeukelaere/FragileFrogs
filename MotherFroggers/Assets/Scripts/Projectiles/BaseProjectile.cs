using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class BaseProjectile : MonoBehaviour
{
    private Vector3 _target;

    void Init(Vector3 target)
    {
        _target = target;
    }

    private void Update()
    {
        UpdateProjectile();
    }

    abstract protected void UpdateProjectile();
}
