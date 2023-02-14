using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathTile : BaseTile
{
    private GameObject _egg = null;
    public GameObject Egg
    {
        get { return _egg; }
        set { _egg = value; }
    }
}
