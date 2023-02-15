using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clickable : MonoBehaviour
{
    [SerializeField] private bool _clickableOnSpawn = false;

    private bool _isClickable = false;
    public bool IsClickable
    {
        get { return _isClickable; }
        set { _isClickable = value; }
    }

    private bool _isSelected = false;
    public bool IsSelected
    {
        get { return _isSelected; }
        set { _isSelected = value; }
    }

    private void Start()
    {
        _isClickable = _clickableOnSpawn;
    }
}
