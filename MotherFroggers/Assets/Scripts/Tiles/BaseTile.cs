using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseTile : MonoBehaviour
{
    [SerializeField]
    private Transform _socket;
    public Transform Socket
    {
        get { return _socket; }
    }
}
