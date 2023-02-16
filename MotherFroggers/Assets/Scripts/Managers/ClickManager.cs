using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickManager : MonoBehaviour
{
    [SerializeField] ItemManager _itemManager = null;

    private bool _handleClicks = true;
    public bool HandleClicks
    {
        set { _handleClicks = value; }
    }

    private const string HANDLE_CLICK_METHOD = "HandleClick";

    private void Update()
    {
        //Check if we should check the click
        if (_itemManager == null) return;

        if (Input.GetMouseButtonDown(0))
            Invoke(HANDLE_CLICK_METHOD, 0.1f);
    }

    private void HandleClick()
    {
        if (_handleClicks == false)
        {
            _handleClicks = true;
            return;
        }

        if (_itemManager.CurrentItem != null)
        {
            //Handle placing the current item
            _itemManager.HandlePlacingItem();
        }
        else
        {
            //Handle selecting an egg
            _itemManager.HandleSelectingEgg();
        }
    }
}
