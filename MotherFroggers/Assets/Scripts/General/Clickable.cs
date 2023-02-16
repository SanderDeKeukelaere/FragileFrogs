using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clickable : MonoBehaviour
{
    [SerializeField] private bool _clickableOnSpawn = false;
    [SerializeField] private GameObject _clickableIndicator = null;

    private bool _isClickable = false;
    public bool IsClickable
    {
        get { return _isClickable; }
        set { _isClickable = value; }
    }

    private void Start()
    {
        _isClickable = _clickableOnSpawn;
    }

    public void SetClickableIndicatorState(bool shouldShow)
    {
        //Make sure we have an indicator
        if (_clickableIndicator == null) return;

        //Set state of the indicator
        if (_isClickable == false)
            _clickableIndicator.SetActive(shouldShow);
    }
}
