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
        {
            if (_itemManager.CurrentItem != null)
            {
                Debug.Log("Handle placing item");
                //Handle placing the current item
                _itemManager.HandlePlacingItem();
            }
            else
            {
                Debug.Log("Handle selecting egg");
                //Handle selecting an egg
                _itemManager.HandleSelectingEgg();
            }
        }
    }
}
