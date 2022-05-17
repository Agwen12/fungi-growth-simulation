using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class GridCell : MonoBehaviour
{
    private Dictionary<Direction, GridCell> _neighbors = new Dictionary<Direction, GridCell>();
    private GridState _state;
    private float _nutritionLevel;
    private int _x;
    private int _y;
    private int _z;

    public GridCell(int x, int y, int z)
    {
        _x = x;
        _y = y;
        _z = z;
    }

    public void AddNeighbor(Direction direction, GridCell neighbor)
    {
        _neighbors.Add(direction, neighbor);
    }

    public void SetState(GridState state)
    {
        _state = state;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
