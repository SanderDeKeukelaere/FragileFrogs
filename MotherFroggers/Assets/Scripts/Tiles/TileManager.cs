using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    private List<Vector3> _path;
    public List<Vector3> Path
    {
        get { return _path; }
    }

    private List<BaseTile> _pathTiles = new List<BaseTile>();
    public List<BaseTile> PathTiles
    {
        get { return _pathTiles; }
    }
    private List<BaseTile> _worldTiles = new List<BaseTile>();
    public List<BaseTile> WorldTiles
    {
        get { return _worldTiles; }
    }

    private bool[,] _tileData;
    private int _width, _height;

    private int _startTileX, _startTileZ;
    private int _endTileX, _endTileZ;

    [SerializeField] GameObject _debugObject;

    private void Awake()
    {

        BaseTile[] objTiles = FindObjectsByType<BaseTile>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);

        int minX = int.MaxValue, minZ = int.MaxValue;
        int maxX = int.MinValue, maxZ = int.MinValue;

        foreach(BaseTile tile in objTiles)
        {
            if ((int)tile.transform.position.x > maxX)
            {
                maxX = (int)tile.transform.position.x;
            }
            if ((int)tile.transform.position.z > maxZ)
            {
                maxZ = (int)tile.transform.position.z;
            }
            if ((int)tile.transform.position.x < minX)
            {
                minX = (int)tile.transform.position.x;
            }
            if ((int)tile.transform.position.z < minZ)
            {
                minZ = (int)tile.transform.position.z;
            }

            if (tile.GetComponent<PathTile>())
            {
                //Add tile to path tile list
                _pathTiles.Add(tile);

                if (tile.GetComponent<PathTile>().IsStart)
                {
                    _startTileX = (int)tile.transform.position.x;
                    _startTileZ = (int)tile.transform.position.z;
                }
                else if (tile.GetComponent<PathTile>().IsEnd)
                {
                    _endTileX = (int)tile.transform.position.x;
                    _endTileZ = (int)tile.transform.position.z;
                }
            }
            else
            {
                //Add tile to world tile list
                _worldTiles.Add(tile);
            }
        }

        _width = maxX - minX + 1;
        _height = maxZ - minZ + 1;
        _tileData = new bool[_width,_height];

        for (int x = 0; x < _width; x++)
        {
            for (int z = 0; z < _height; z++)
            {
                bool found = false;
                foreach (BaseTile tile in objTiles)
                {
                    if ((int)tile.transform.position.x == x + minX && (int)tile.transform.position.z == z + minZ)
                    {
                        if (tile.gameObject.GetComponent<PathTile>())
                        {
                            _tileData[x,z] = true;
                            found = true;
                            break;
                        }
                    }
                }
                if (found == false)
                {
                    _tileData[x,z] = false;
                }
            }
        }

        _startTileX -= minX;
        _startTileZ -= minZ;
        _endTileX -= minX;
        _endTileZ -= minZ;

        //string map = "";
        //for (int x = 0; x < _width; x++)
        //{
        //    for (int z = 0; z < _height; z++)
        //    {
        //        if (_tileData[x, z])
        //        {
        //            map += "1";
        //        }
        //        else
        //        {
        //            map += "0";
        //        }
        //    }
        //    map += "\n";
        //}
        //Debug.Log(map);

        List<Node> pathNodes = AStar.FindPath(_startTileX, _startTileZ, _endTileX, _endTileZ, _tileData);
        _path = new List<Vector3>();
        if (pathNodes != null)
        {
            int id = 0;
            foreach (Node node in pathNodes)
            {
                id++;
                Vector2 position = Vector2.zero;
                position.x = node.X;
                position.y = node.Y;
                _path.Add(new Vector3(node.X + minX, 0, node.Y + minZ));
                if (_debugObject)
                {
                    Instantiate(_debugObject, new Vector3(node.X + minX, 0, node.Y + minZ), Quaternion.identity).name = $"path{id}";
                }
            }
        }
        else
        {
            Debug.LogError($"No path found");
        }
    }

    public void SetClickableTilesOfType(MyEnums.TileType tileType)
    {
        //Set all tiles of given type clickable
        switch (tileType)
        {
            case MyEnums.TileType.PATH:
                {
                    foreach (BaseTile tile in _pathTiles)
                    {
                        tile.GetComponent<Clickable>().IsClickable = true;
                    }

                    break;
                }

            case MyEnums.TileType.WORLD:
                {
                    foreach (BaseTile tile in _worldTiles)
                    {
                        tile.GetComponent<Clickable>().IsClickable = true;
                    }

                    break;
                }

            default:
                break;
        }
    }

    public void SetClickableTilesOfTypeInRange(MyEnums.TileType tileType, Vector3 center, int range)
    {
        //Find all tiles of given type in the given range
        Collider[] tilesInRange = Physics.OverlapBox(center, new Vector3((float)range, 1.0f, (float)range), Quaternion.identity, LayerMask.GetMask("Tile"));

        foreach (Collider collider in tilesInRange)
        {
            //Make sure we only check tiles of the given type
            switch (tileType)
            {
                case MyEnums.TileType.PATH:
                    {
                        PathTile tile = collider.GetComponent<PathTile>();
                        if (tile == null) continue;

                        //Check there is no item on the tile already
                        if (tile.Item != null) continue;

                        tile.GetComponent<Clickable>().IsClickable = true;

                        break;
                    }

                case MyEnums.TileType.WORLD:
                    {
                        WorldTile tile = collider.GetComponent<WorldTile>();
                        if (tile == null) continue;

                        //Check there is no item on the tile already
                        if (tile.Item != null) continue;

                        tile.GetComponent<Clickable>().IsClickable = true;

                        break;
                    }

                default:
                    break;
            }
        }
    }

    public void DisableAllClickableTiles()
    {
        foreach (BaseTile tile in _pathTiles)
        {
            tile.GetComponent<Clickable>().IsClickable = false;
        }
        foreach (BaseTile tile in _worldTiles)
        {
            tile.GetComponent<Clickable>().IsClickable = false;
        }
    }

    public void SetClickableIndicatorState(bool shouldShow)
    {
        foreach (BaseTile tile in _pathTiles)
        {
            tile.GetComponent<Clickable>().SetClickableIndicatorState(shouldShow);
        }
        foreach (BaseTile tile in _worldTiles)
        {
            tile.GetComponent<Clickable>().SetClickableIndicatorState(shouldShow);
        }
    }
}
public class Node
{
    public int X { get; set; }
    public int Y { get; set; }
    public double G { get; set; } // cost to move from start to this node
    public double H { get; set; } // estimated cost to move from this node to end
    public double F { get { return G + H; } } // estimated total cost to move from start to end through this node
    public Node Parent { get; set; } // node used to reach this node
    public Node(int x, int y)
    {
        X = x;
        Y = y;
    }
    public override string ToString()
    {
        return $"X: {X} Y:{Y}";
    }
    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        return X == ((Node)obj).X && Y == ((Node)obj).Y;
    }
}

public class AStar
{
    public static List<Node> FindPath(int startX, int startY, int endX, int endY, bool[,] map)
    {
        // create lists to store nodes to be explored and nodes already explored
        List<Node> openList = new List<Node>();
        List<Node> closedList = new List<Node>();

        // create start and end nodes
        Node startNode = new Node(startX, startY);
        Node endNode = new Node(endX, endY);

        // add start node to open list
        openList.Add(startNode);

        // while the open list is not empty
        while (openList.Count > 0)
        {
            if (startNode.Equals(endNode))
            {
                break;
            }

            // find the node in the open list with the lowest F value
            Node currentNode = openList[0];
            for (int i = 1; i < openList.Count; i++)
            {
                if (openList[i].F < currentNode.F)
                {
                    currentNode = openList[i];
                }
            }

            // remove the current node from the open list and add it to the closed list
            openList.Remove(currentNode);
            closedList.Add(currentNode);

            // if the current node is the end node, we have found a path
            if (currentNode.Equals(endNode))
            {
                // construct the path by backtracking from the end node to the start node
                return ConstructPath(currentNode);
            }

            // get a list of the current node's neighbors
            List<Node> neighbors = GetNeighbors(currentNode, map);

            // for each neighbor of the current node
            foreach (Node neighbor in neighbors)
            {
                // if the neighbor is in the closed list, ignore it
                if (closedList.Contains(neighbor))
                {
                    continue;
                }

                // calculate the cost to move from the start node to this neighbor
                double cost = currentNode.G + GetDistance(currentNode, neighbor);

                // if the neighbor is not in the open list, or if the cost to move to this neighbor is
                // lower than its current cost, set the neighbor's G value to the cost and set its
                // parent to the current node
                if (!openList.Contains(neighbor) || cost < neighbor.G)
                {
                    neighbor.G = cost;
                    neighbor.H = GetDistance(neighbor, endNode);
                    neighbor.Parent = currentNode;
                    // if the neighbor is not in the open list, add it to the open list
                    if (!openList.Contains(neighbor))
                    {
                        openList.Add(neighbor);
                        Console.WriteLine(neighbor);
                    }
                }
            }
        }
        // if the open list is empty and we have not found the end node, there is no path
        return null;
    }

    private static List<Node> ConstructPath(Node endNode)
    {
        // create a list to store the path
        List<Node> path = new List<Node>();

        // add the end node to the path
        path.Add(endNode);

        // set the current node to the end node's parent
        Node currentNode = endNode.Parent;

        // while the current node is not the start node
        while (currentNode != null)
        {
            // add the current node to the path
            path.Add(currentNode);

            // set the current node to its parent
            currentNode = currentNode.Parent;
        }

        // reverse the list to get the path from start to end
        path.Reverse();

        return path;
    }

    private static List<Node> GetNeighbors(Node node, bool[,] map)
    {
        // create a list to store the neighbors
        List<Node> neighbors = new List<Node>();

        // add nodes to the list if they are walkable and within the bounds of the map
        if (IsWalkable(node.X - 1, node.Y, map))
        {
            neighbors.Add(new Node(node.X - 1, node.Y));
        }
        if (IsWalkable(node.X + 1, node.Y, map))
        {
            neighbors.Add(new Node(node.X + 1, node.Y));
        }
        if (IsWalkable(node.X, node.Y - 1, map))
        {
            neighbors.Add(new Node(node.X, node.Y - 1));
        }
        if (IsWalkable(node.X, node.Y + 1, map))
        {
            neighbors.Add(new Node(node.X, node.Y + 1));
        }

        return neighbors;
    }

    private static bool IsWalkable(int x, int y, bool[,] map)
    {
        // return true if the coordinates are within the bounds of the map and the tile is walkable
        return x >= 0 && x < map.GetLength(0) && y >= 0 && y < map.GetLength(1) && map[x, y];
    }

    private static double GetDistance(Node node1, Node node2)
    {
        // use the Manhattan distance heuristic to estimate the distance between two nodes
        return Math.Abs(node1.X - node2.X) + Math.Abs(node1.Y - node2.Y);
    }
}