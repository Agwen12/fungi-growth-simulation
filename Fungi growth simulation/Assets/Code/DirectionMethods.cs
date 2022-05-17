using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public static class DirectionMethods
{
    private static Dictionary<Direction, int[,]> _directionToOffsets = new Dictionary<Direction, int[,]>()
    {
        { Direction.FORWARD, new int[,] { { 0, 1, 0 }, { 0, 1, 0 } } },
        { Direction.BACK, new int[,] { { 0, -1, 0 }, { 0, -1, 0 } } },
        { Direction.LEFT, new int[,] { { -1, 0, 0 }, { -1, 0, 0 } } },
        { Direction.RIGHT, new int[,] { { 1, 0, 0 }, { 1, 0, 0 } } },
        { Direction.UP, new int[,] { { 0, 0, 2 }, { 0, 0, 2 } } },
        { Direction.DOWN, new int[,] { { 0, 0, -2 }, { 0, 0, -2 } } },
        { Direction.UP_FORWARD_RIGHT, new int[,] { { 0, 0, 1 }, { 1, 1, 1 } } },
        { Direction.UP_FORWARD_LEFT, new int[,] { { -1, 0, 1 }, { 0, 1, 1 } } },
        { Direction.UP_BACK_RIGHT, new int[,] { { 0, -1, 1 }, { 1, 0, 1 } } },
        { Direction.UP_BACK_LEFT, new int[,] { { -1, -1, 1 }, { 0, 0, 1 } } },
        { Direction.DOWN_FORWARD_RIGHT, new int[,] { { 0, 0, -1 }, { 1, 1, -1 } } },
        { Direction.DOWN_FORWARD_LEFT, new int[,] { { -1, 0, -1 }, { 0, 1, -1 } } },
        { Direction.DOWN_BACK_RIGHT, new int[,] { { 0, -1, -1 }, { 1, 0, -1 } } },
        { Direction.DOWN_BACK_LEFT, new int[,] { { -1, -1, -1 }, { 0, 0, -1 } } },
    };

    private static int[,] GetOffsets(Direction direction)
    {
        return _directionToOffsets[direction];
    }

    public static int[] GetOffsetPosition(int[] currPosition, Direction direction)
    {
        int currLayerParity = currPosition[2] % 2;
        int[,] offsets = GetOffsets(direction); // No entiendo porque el function? // jeste? w Polsce pisz po polsku, wrapper na dicta
        int[] offset = Enumerable.Range(0, offsets.GetLength(1))
                                 .Select(x => offsets[currLayerParity, x])
                                 .ToArray();
        int[] offsetPosition = { currPosition[0] + offset[0], 
                                 currPosition[1] + offset[1], 
                                 currPosition[2] + offset[2] };
        return offsetPosition;
    }

/*    public static Direction GetRandomDirection()
    {
        Array values = Enum.GetValues(typeof(Direction));
        return (Direction)values.GetValue(Helper.Rnd.Next(values.Length));
    }*/
}
