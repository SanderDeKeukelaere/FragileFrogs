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

    public void HandleClick()
    {
        if (_currentItem != null)
            HandleTileClick();
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

        return hitObject.GetComponent<Clickable>().IsClickable;
    }

    private void HandleTileClick()
    {
        //Ignore invalid clicks
        if (IsValidClick(out GameObject hitObject) == false)
            return;

        //Add the current item to the tile
        BaseTile tile = hitObject.GetComponent<BaseTile>();

        if (tile != null)
        {
            _currentItem.transform.position = tile.Socket.position;
            tile.Item = _currentItem;
            _currentItem.transform.parent = tile.transform;
        }
    }
}
