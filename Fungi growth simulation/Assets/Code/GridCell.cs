using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class GridCell
{
    private Dictionary<Direction, GridCell> _neighbors = new Dictionary<Direction, GridCell>();
    private GridState _state;
    private float _nutritionLevel;
    private int _x;
    private int _y;
    private int _z;
    private GameObject _gameObject;
    private static GameObject _prefab = GameObject.Instantiate(Resources.Load("GridCell")) as GameObject;
    private static Vector3 _prefabSize = (_prefab.GetComponent(typeof(Renderer)) as Renderer).bounds.size;

    public GridCell(int x, int y, int z)
    {
        _state = GridState.EMPTY;
        _x = x;
        _y = y;
        _z = z;
        Vector3 position = new Vector3(_x * _prefabSize.x, _y * _prefabSize.y, _z * _prefabSize.z);
        if (z % 2 == 1)
        {
            position += new Vector3(Config.LayersOffsetsPerc[0] * _prefabSize.x,
                                    Config.LayersOffsetsPerc[1] * _prefabSize.y,
                                    Config.LayersOffsetsPerc[2] * _prefabSize.z);
        }
        _gameObject = GameObject.Instantiate(_prefab, position, Quaternion.identity);
    }

    public void AddNeighbor(Direction direction, GridCell neighbor)
    {
        _neighbors.Add(direction, neighbor);
    }

    public void SetState(GridState state)
    {
        _state = state;
    }

    public void Update()
    {
        _gameObject.GetComponent<Renderer>()
                   .material
                   .SetColor("_Color", GridStateMethods.toColor(_state));
        _gameObject.SetActive(_state != GridState.EMPTY);
    }
}
