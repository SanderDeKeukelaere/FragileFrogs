using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickManager : MonoBehaviour
{
    [SerializeField] ItemManager _itemManager = null;
    

    private void Update()
    {
        //Check if we should check the click
        if (_itemManager == null) return;

        if (Input.GetMouseButtonDown(0))
            HandleClick();
    }

    private void HandleClick()
    {
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
