using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class GridCell : MonoBehaviour
{
    private Dictionary<Direction, GridCell> _neighbors = new Dictionary<Direction, GridCell>();
    private GridState _state;
    private float _nutritionLevel;

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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
