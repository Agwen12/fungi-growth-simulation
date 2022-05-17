using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class GridCell : MonoBehaviour
{
    private Dictionary<Direction, GridCell> _neighbors = new Dictionary<Direction, GridCell>();
    private GridState _state;
    private float _nutritionLevel;
    private int x;
    private int y;
    private int z;

    public GridCell(int x, int y, int z)
    {
        // foreach (Direction direction in Enum.GetValues(typeof(Direction)))
        // {
        //     Vector3 directionVector3 = DirectionMethods.ToVector3(direction);
        //     int[] neighborPosition = { x + (int)directionVector3[0], 
        //                                y + (int)directionVector3[1], 
        //                                z + (int)directionVector3[2] };
        //     if (Grid.IsPositionValid(neighborPosition))
        //         _neighbors.Add(direction, )
        // }
    }

    public AddNeighbors(GridCell[,,] gridCells)
    {
        foreach (Direction direction in Enum.GetNames<Direction>)
        {
            // TODO make sure it compiles, cuz I'm not sure 
            int[] newPos = DirectionMethods.GetNewPosition({x, y, z}, direction);
            if (newPos[0] >= 0 && newPos[1] >= 0 && newPos[2] >= 0);
            _neighbors.Add(direction, gridCells[newPos[0], newPos[1], newPos[2]]);
        }
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
