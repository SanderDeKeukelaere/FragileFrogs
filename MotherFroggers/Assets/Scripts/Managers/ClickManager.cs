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
                //Handle the click
                _itemManager.HandleClick();
            }
            else
            {
                //Handle selecting an item
                //...
            }
        }
    }
}
