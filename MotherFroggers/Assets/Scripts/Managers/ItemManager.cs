using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    private float _maxHitDistance = 5.0f;

    private GameObject _currentItem = null;
    public GameObject CurrentItem
    {
        get { return _currentItem; }
        set { _currentItem = value; }
    }

    public void HandlePlacingItem()
    {
        //Check if we have an item to place
        if (_currentItem == null)
            return;

        //Check if we clicked a tile
        if (IsValidClick(out GameObject hitObject) == false)
            return;
        
        if (hitObject == null) return;

        //Check if the clicked tile is clickable
        if (hitObject.GetComponent<Clickable>().IsClickable == false) return;
        Debug.Log("Clickable tile hit");

        //Place the current item on the tile
        BaseTile tile = hitObject.GetComponent<BaseTile>();
        if (tile == null) return;
        Debug.Log("Moved item to clicked tile");

        _currentItem.transform.position = tile.Socket.position;
        _currentItem.transform.parent = tile.transform;
    }

    public void HandleSelectingEgg()
    {
        //Check if we do not have an item to place
        if (_currentItem != null)
            return;

        //Check if we clicked a tile
        if (IsValidClick(out GameObject hitObject) == false)
            return;
        
        if (hitObject == null) return;

        //Check if the clicked tile contains an egg
        PathTile pathTile = hitObject.GetComponent<PathTile>();
        if (pathTile == null) return;
        Debug.Log("PathTile hit");

        GameObject itemOnTile = pathTile.Item;
        if (itemOnTile == null) return;

        Egg eggOnTile = itemOnTile.GetComponent<Egg>();
        if (eggOnTile == null) return;
        Debug.Log("Tile contains egg");

        //Check if the egg is ready to hatch
        /*if (eggOnTile.IsReadyToHatch)
        {
            Debug.Log("Egg is ready to hatch");
            //If so, hatch the egg
            GameObject hatchedItem = eggOnTile.Hatch();
            if (hatchedItem == null) return;

            //Detach the egg from clicked tile
            pathTile.Item = null;

            //Select the hatched item as current item
            _currentItem = hatchedItem;
            _currentItem.transform.position = pathTile.Socket.position;
            _currentItem.transform.parent = pathTile.transform;
        }*/
    }

    private bool IsValidClick(out GameObject hitObject)
    {
        //Get the ray from mouse positon
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        //Ignore other checks if we didn't hit anything
        if (Physics.Raycast(ray, out RaycastHit hitInfo, _maxHitDistance, LayerMask.GetMask("Tile")) == false)
        {
            hitObject = null;
            return false;
        }

        //Check if we hit a clickable object
        hitObject = hitInfo.collider.gameObject;

        return true;
    }
}
