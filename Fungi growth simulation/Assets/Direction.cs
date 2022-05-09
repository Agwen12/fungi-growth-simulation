using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction
{
    FORWARD,
    BACK,
    LEFT,
    RIGHT,
    UP,
    DOWN,
    UP_FORWARD_RIGHT,
    UP_FORWARD_LEFT,
    UP_BACK_RIGHT,
    UP_BACK_LEFT,
    DOWN_FORWARD_RIGHT,
    DOWN_FORWARD_LEFT,
    DOWN_BACK_RIGHT,
    DOWN_BACK_LEFT
}

public static class DirectionMethods
{
/*    public static HashSet<(Direction, Vector3)> DirectionDict = new HashSet<(Direction, Vector3)>
    { () }*/

    public static Vector3 ToVector3(Direction direction)
    {
        return direction switch
        {
            Direction.FORWARD => new Vector3(0, 0, 1),
            Direction.BACK => new Vector3(0, 0, -1),
            Direction.LEFT => new Vector3(-1, 0, 0),
            Direction.RIGHT => new Vector3(1, 0, 0),
            Direction.UP => new Vector3(0, 1, 0),
            Direction.DOWN => new Vector3(0, -1, 0),
            Direction.UP_FORWARD_RIGHT => new Vector3(1, 1, 1),
            Direction.UP_FORWARD_LEFT => new Vector3(-1, 1, 1),
            Direction.UP_BACK_RIGHT => new Vector3(1, 1, -1),
            Direction.UP_BACK_LEFT => new Vector3(-1, 1, -1),
            Direction.DOWN_FORWARD_RIGHT => new Vector3(1, -1, 1),
            Direction.DOWN_FORWARD_LEFT => new Vector3(-1, -1, 1),
            Direction.DOWN_BACK_RIGHT => new Vector3(1, -1, -1),
            Direction.DOWN_BACK_LEFT => new Vector3(-1, -1, -1),
            _ => new Vector3(0, 0, 0)
        };
    }



    public static List<Vector3> GetNeighbors(Direction direction)
    {
        List<Vector3> neighbors = new List<Vector3>();
        Vector3 directionVector3 = ToVector3(direction);
        float[] coords = { 1, 1, 1 };

        if (Mathf.Abs(directionVector3.x) + Mathf.Abs(directionVector3.y) + Mathf.Abs(directionVector3.z) == 1) // 4 sides
        {
            // replace 0s with possible values(all permutations)
            float[,] replacements = { { 1, 1 }, { -1, 1 }, { 1, -1 }, { -1, -1 } };
            for (int i = 0; i < 4; i++)
            {
                int ri = 0;
                for (int j = 0; j < 3; j++)
                    coords[i] = directionVector3[i] == 0 ? coords[i] = replacements[i,ri++] : directionVector3[i];
                neighbors.Add(new Vector3(coords[0], coords[1], coords[2]));
            }
        }
        else // 6 sides
        {
            // keep one coordinate, set rest to 0
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                    coords[j] = 0;
                coords[i] = directionVector3[i];
                neighbors.Add(new Vector3(coords[0], coords[1], coords[2]));

            }

            // keep all coordinates, multiply one by -1
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                    coords[j] = directionVector3[i];
                coords[i] = -1 * directionVector3[i];
                neighbors.Add(new Vector3(coords[0], coords[1], coords[2]));
            }
        }

        return neighbors;
    }

    public static Direction GetRandomDirection()
    {
        var values = Direction.GetNames(typeof(Direction));
        return (Direction)values.GetValue(Helper.Rnd.Next(values.Length));
    }

    public static Direction GetRandomDirection(Direction direction)
    {
        List<Vector3> neighbors = DirectionMethods.GetNeighbors(direction);
        return neighbors[Helper.Rnd.Next(neighbors.Count)];
    }
}
