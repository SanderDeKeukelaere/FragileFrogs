using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeSizer : MonoBehaviour
{

    [SerializeField] GameObject _tree = null;

    void Start()
    {
        if (!_tree) return;

        float randomScale = Random.Range(8f, 12.5f);

        _tree.transform.localScale = new Vector3(randomScale, randomScale, randomScale);

        float randomPositionX = Random.Range(-0.15f, 0.15f);
        float randomPositionY = Random.Range(-0.15f, 0.15f);

        Vector3 offsetPos = new Vector3(randomPositionX, 0, randomPositionY);

        _tree.transform.localPosition += offsetPos;

        float randomRotationY = Random.Range(-180f, 180f);
        float randomRotationX = Random.Range(-10, 10f);
        float randomRotationZ = Random.Range(-10f, 10f);

        Vector3 localRotation = new Vector3(-90 + randomRotationX, randomRotationY, randomRotationZ);

        _tree.transform.localEulerAngles = localRotation;
    }

}
