using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
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
    private static Dictionary<Direction, Vector3> _directionToVector3 = new Dictionary<Direction, Vector3>()
    {
        { Direction.FORWARD, new Vector3(0, 0, 1) },
        { Direction.BACK, new Vector3(0, 0, -1) },
        { Direction.LEFT, new Vector3(-1, 0, 0) },
        { Direction.RIGHT, new Vector3(1, 0, 0) },
        { Direction.UP, new Vector3(0, 1, 0) },
        { Direction.DOWN, new Vector3(0, -1, 0) },
        { Direction.UP_FORWARD_RIGHT, new Vector3(1, 1, 1) },
        { Direction.UP_FORWARD_LEFT, new Vector3(-1, 1, 1) },
        { Direction.UP_BACK_RIGHT, new Vector3(1, 1, -1) },
        { Direction.UP_BACK_LEFT, new Vector3(-1, 1, -1) },
        { Direction.DOWN_FORWARD_RIGHT, new Vector3(1, -1, 1) },
        { Direction.DOWN_FORWARD_LEFT, new Vector3(-1, -1, 1) },
        { Direction.DOWN_BACK_RIGHT, new Vector3(1, -1, -1) },
        { Direction.DOWN_BACK_LEFT, new Vector3(-1, -1, -1) }
    };
    private static Dictionary<Vector3, Direction> _vector3ToDirection = _directionToVector3.ToDictionary((i) => i.Value, (i) => i.Key);
    
    private static Direction FromCoords(float[] coords)
    {
        Vector3 vector3 = new Vector3(coords[0], coords[1], coords[2]);
        return FromVector3(vector3);
    }

    public static Vector3 ToVector3(Direction direction)
    {
        return (Vector3)_directionToVector3[direction];
    }

    public static Direction FromVector3(Vector3 vector3)
    {
        return (Direction)_vector3ToDirection[vector3];
    }

    public static List<Direction> GetNeighbors(Direction direction)
    {
        List<Direction> neighbors = new List<Direction>();
        Vector3 directionVector3 = ToVector3(direction);
        float[] coords = { 1, 1, 1 };

        if (DirectionMethods.IsSquare(directionVector3)) // 4 sides
        {
            // replace 0s with possible values(all permutations)
            float[,] replacements = { { 1, 1 }, { -1, 1 }, { 1, -1 }, { -1, -1 } };
            for (int i = 0; i < 4; i++)
            {
                int ri = 0;
                for (int j = 0; j < 3; j++)
                    coords[j] = directionVector3[j] == 0 ? coords[j] = replacements[j,ri++] : directionVector3[j];
                neighbors.Add(FromCoords(coords));
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
                neighbors.Add(FromCoords(coords));

            }

            // keep all coordinates, multiply one by -1
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                    coords[j] = directionVector3[i];
                coords[i] = -1 * directionVector3[i];
                neighbors.Add(FromCoords(coords));
            }
        }

        return neighbors;
    }

    public static Direction GetRandomDirection()
    {
        Array values = Enum.GetValues(typeof(Direction));
        return (Direction)values.GetValue(Helper.Rnd.Next(values.Length));
    }

    public static Direction GetRandomDirection(Direction direction)
    {
        List<Direction> neighbors = GetNeighbors(direction);
        return neighbors[Helper.Rnd.Next(neighbors.Count)];
    }


    private static bool IsSquare(Vector3 vector) {
        return vector.sqrMagnitude == 1;
    }

    public static Vector3[] GetBranchDirection(Direction direction) {
        Vector3 vector = ToVector3(direction);
        if(DirectionMethods.IsSquare(vector))
        {
            int[] units = {1 - 2 * Helper.Rnd.Next(0, 1), 1 - 2 * Helper.Rnd.Next(0, 1) };
            int unitIdx = 0;
            Vector3 branch1 = new Vector3(1, 1, 1);
            Vector3 branch2 = new Vector3(1, 1, 1);
            for (int i = 0; i < 3; i++)
            {
                if (vector[i] == 0)
                {
                    branch1[i] = units[unitIdx];
                    branch2[i] = -units[unitIdx];
                    unitIdx++;
                }
                else
                {
                    branch1[i] = vector[i];
                    branch2[i] = vector[i];
                }
            }

            return  new Vector3[] { branch1, branch2 };
        }
        else {
            int fixedFace = Helper.Rnd.Next(0, 2);
            Vector3 squareFace = new Vector3(0, 0, 0);
            Vector3 hexFace = new Vector3(0, 0, 0);
            squareFace[fixedFace] = vector[fixedFace];
            for (int i = 0; i < 3; i++)
            {
                if(i != fixedFace) 
                {
                    hexFace[i] = vector[i];
                } else {
                    hexFace[i] = -squareFace[i];
                }
            }
            return new Vector3[] { squareFace, hexFace };
        }
    }
}
