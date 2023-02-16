using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentsRandom : MonoBehaviour
{
    [SerializeField] private float _maxTranslateOffset = 1f;
    [SerializeField] private float _maxRotationAngle = 180f;
    [SerializeField] private Vector2 _scaleFactorRange = new Vector2(1f, 1.5f);

    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.position += new Vector3(Random.Range(-_maxTranslateOffset, _maxTranslateOffset), 0, Random.Range(-_maxTranslateOffset, _maxTranslateOffset));

        Vector3 rot = gameObject.transform.eulerAngles;
        rot.y += Random.Range(-_maxRotationAngle, _maxRotationAngle);
        gameObject.transform.eulerAngles = rot;

        float scaleFactor = Random.Range(_scaleFactorRange.x, _scaleFactorRange.y);
        gameObject.transform.localScale = new Vector3(scaleFactor, scaleFactor, scaleFactor);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
