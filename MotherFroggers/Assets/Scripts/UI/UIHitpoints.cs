using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHitpoints : MonoBehaviour
{
    [SerializeField] GameObject[] _hitpoints = new GameObject[10];
    Motherfrogger _motherfrogger;


    private void Awake()
    {
        _motherfrogger = FindAnyObjectByType<Motherfrogger>();
        if (_motherfrogger == null)
        {
            Debug.LogError("Motherfrogger not found");
        }
    }
    void Update()
    {
        for (int i = 0; i < _hitpoints.Length; i++)
        {
            _hitpoints[i].SetActive(_motherfrogger.CurrentHp > i);
        }
    }
}
