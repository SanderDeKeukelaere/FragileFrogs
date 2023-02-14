using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickManager : MonoBehaviour
{
    [SerializeField] ItemPlacement _itemPlacement = null;

    private void Update()
    {
        //Check if we should check the click
        if (_itemPlacement == null) return;

        if (Input.GetMouseButtonDown(0))
        {
            //Handle the click
            _itemPlacement.HandleClick();
        }
    }
}
