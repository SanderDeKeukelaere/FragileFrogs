using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldTile : BaseTile
{
    private GameObject _tower = null;
    public GameObject Tower
    {
        get { return _tower; }
        set { _tower = value; }
    }
}
