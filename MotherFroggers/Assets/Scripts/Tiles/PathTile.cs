using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathTile : BaseTile
{
    [SerializeField] bool _isStart = false;
    [SerializeField] bool _isEnd = false;

    public bool IsStart { get { return _isStart; } }
    public bool IsEnd { get { return _isEnd; } }
}
