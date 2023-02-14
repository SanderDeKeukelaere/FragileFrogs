using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Clickable))]
public class BaseTile : MonoBehaviour
{
    [SerializeField] protected Transform _socket;
    public Transform Socket
    {
        get { return _socket; }
    }

    protected GameObject _item = null;
    public GameObject Item
    {
        get { return _item; }
        set { _item = value; }
    }

    protected Clickable _clickableComponent = null;

    private void Awake()
    {
        _clickableComponent = GetComponent<Clickable>();
    }
}
