using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    [SerializeField] private int _hatchedRange = 3;

    private float _maxHitDistance = 40.0f;

    private GameObject _currentItem = null;
    public GameObject CurrentItem
    {
        get { return _currentItem; }
        set { _currentItem = value; }
    }

    private BaseTile _currentTile = null;

    private bool _hasValidPlacement = false;
    public bool HasValidPlacement
    {
        get { return _hasValidPlacement; }
    }

    private TileManager _tileManager = null;

    private void Start()
    {
        _tileManager = GetComponent<TileManager>();
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

        BaseTile tile = hitObject.GetComponent<BaseTile>();
        if (tile == null) return;

        //Check if the clicked tile doesn't already have an item on it
        if (tile.Item != null) return;

        //Place the current item on the tile
        _currentItem.transform.parent = tile.Socket;
        _currentItem.transform.localPosition = Vector3.zero;

        //Save which tile the item is currently on
        _currentTile = tile;
        _hasValidPlacement = true;
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

        GameObject itemOnTile = pathTile.Item;
        if (itemOnTile == null) return;

        Egg eggOnTile = itemOnTile.GetComponent<Egg>();
        if (eggOnTile == null) return;

        //Handle hatching the egg
        if (HandleEggHatching(pathTile, eggOnTile) == false) return;

        //Set all tiles in the hatched egg's range clickable
        TileManager tileManager = GetComponent<TileManager>();
        if (tileManager == null) return;

        List<MyEnums.TileType> itemPlaceableOn = _currentItem.GetComponent<BaseTower>().PlaceableOn;
        foreach (MyEnums.TileType tileType in itemPlaceableOn)
        {
            tileManager.SetClickableTilesOfTypeInRange(tileType, pathTile.transform.position, _hatchedRange);
        }
    }

    public bool ConfirmItemPlacement()
    {
        if (!_currentTile) return false;

        //Only continue if the placement is valid and if there is a tile manager
        TileManager tileManager = GetComponent<TileManager>();

        if (_hasValidPlacement == false || tileManager == null) return false;

        //Set the current item as the item of the tile it's currently placed on
        _currentTile.Item = _currentItem;

        //Reset variables
        _currentItem = null;
        _currentTile = null;
        _hasValidPlacement = false;

        //Disable all clickable tiles
        tileManager.DisableAllClickableTiles();

        return true;
    }

    private bool HandleEggHatching(PathTile tile, Egg egg)
    {
        //Check if the egg is ready to hatch
        if (egg.IsReadyToHatch == false)
            return false;


        egg.Hatch();
        //If so, hatch the egg
        //GameObject hatchedItem = egg.Hatch();
        //if (hatchedItem == null) return false;

        //Detach the egg from clicked tile
        tile.Item = null;

        ////Select the hatched item as current item
        //_currentItem = hatchedItem;
        _currentItem.transform.parent = tile.Socket;
        _currentItem.transform.localPosition = Vector3.zero;

        _hasValidPlacement = true;

        return true;
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
