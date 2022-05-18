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
    private static Dictionary<Direction, Direction[]> _directionToAcute = new Dictionary<Direction, Direction[]>()
    {
        { Direction.FORWARD, new Direction[] { Direction.UP_FORWARD_RIGHT, Direction.UP_FORWARD_RIGHT, Direction.UP_FORWARD_RIGHT, Direction.UP_FORWARD_RIGHT } },
        { Direction.BACK, new Direction[] { Direction.UP_BACK_RIGHT, Direction.UP_BACK_RIGHT, Direction.UP_BACK_RIGHT, Direction.UP_BACK_RIGHT } },
        { Direction.LEFT, new Direction[] { Direction.DOWN_BACK_LEFT, Direction.DOWN_BACK_LEFT, Direction.DOWN_BACK_LEFT, Direction.DOWN_BACK_LEFT } },
        { Direction.RIGHT, new Direction[] { Direction.DOWN_BACK_RIGHT, Direction.DOWN_BACK_RIGHT, Direction.DOWN_BACK_RIGHT, Direction.DOWN_BACK_RIGHT } },
        { Direction.UP, new Direction[] { Direction.UP_BACK_RIGHT, Direction.UP_BACK_RIGHT, Direction.UP_BACK_RIGHT, Direction.UP_BACK_RIGHT } },
        { Direction.DOWN, new Direction[] { Direction.DOWN_BACK_RIGHT, Direction.DOWN_BACK_RIGHT, Direction.DOWN_BACK_RIGHT, Direction.DOWN_BACK_RIGHT } },
        { Direction.UP_FORWARD_RIGHT, new Direction[] { Direction.RIGHT, Direction.UP, Direction.FORWARD, Direction.UP_FORWARD_LEFT, Direction.DOWN_FORWARD_RIGHT, Direction.UP_BACK_RIGHT } },
        { Direction.UP_FORWARD_LEFT, new Direction[] { Direction.LEFT, Direction.UP, Direction.FORWARD, Direction.DOWN_BACK_RIGHT, Direction.DOWN_FORWARD_RIGHT, Direction.UP_BACK_RIGHT } },
        { Direction.UP_BACK_RIGHT, new Direction[] { Direction.RIGHT, Direction.UP, Direction.BACK, Direction.UP_FORWARD_LEFT, Direction.DOWN_FORWARD_RIGHT, Direction.DOWN_FORWARD_LEFT } },
        { Direction.UP_BACK_LEFT, new Direction[] { Direction.LEFT, Direction.UP, Direction.BACK, Direction.DOWN_BACK_RIGHT, Direction.DOWN_FORWARD_RIGHT, Direction.DOWN_FORWARD_LEFT } },
        { Direction.DOWN_FORWARD_RIGHT, new Direction[] { Direction.RIGHT, Direction.DOWN, Direction.FORWARD, Direction.UP_FORWARD_LEFT, Direction.UP_BACK_LEFT, Direction.UP_BACK_RIGHT } },
        { Direction.DOWN_FORWARD_LEFT, new Direction[] { Direction.LEFT, Direction.DOWN, Direction.FORWARD, Direction.DOWN_BACK_RIGHT, Direction.UP_BACK_LEFT, Direction.UP_BACK_RIGHT } },
        { Direction.DOWN_BACK_RIGHT, new Direction[] { Direction.RIGHT, Direction.DOWN, Direction.BACK, Direction.UP_FORWARD_LEFT, Direction.UP_BACK_LEFT, Direction.DOWN_FORWARD_LEFT } },
        { Direction.DOWN_BACK_LEFT, new Direction[] { Direction.LEFT, Direction.DOWN, Direction.BACK, Direction.DOWN_BACK_RIGHT, Direction.UP_BACK_LEFT, Direction.DOWN_FORWARD_LEFT } },
    };

    private static int[,] GetOffsets(Direction direction)
    {
        return _directionToOffsets[direction];
    }

    public static int[] GetOffsetPosition(int[] currPosition, Direction direction)
    {
        int currLayerParity = currPosition[2] % 2;
        int[,] offsets = GetOffsets(direction); 
        int[] offset = Enumerable.Range(0, offsets.GetLength(1))
                                 .Select(x => offsets[currLayerParity, x])
                                 .ToArray();
        int[] offsetPosition = { currPosition[0] + offset[0], 
                                 currPosition[1] + offset[1], 
                                 currPosition[2] + offset[2] };
        return offsetPosition;
    }

    public static Direction GetAcute(Direction direction)
    {
        Direction[] acuteDirections = _directionToAcute[direction];
        return acuteDirections[Helper.Rnd.Next(acuteDirections.Length)];
    }

/*    public static Direction GetRandomDirection()
    {
        Array values = Enum.GetValues(typeof(Direction));
        return (Direction)values.GetValue(Helper.Rnd.Next(values.Length));
    }*/
}
