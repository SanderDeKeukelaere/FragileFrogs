using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRandomPrefab : MonoBehaviour
{
    [SerializeField] private List<GameObject> _prefabs;

    // Start is called before the first frame update
    void Start()
    {
        int index = Random.Range(0, _prefabs.Count);
        Instantiate(_prefabs[index], gameObject.transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
