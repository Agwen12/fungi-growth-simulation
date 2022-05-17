using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid
{
    private GridCell[,,] _gridCells;

    public Grid()
    {
        for (int x = 0; x < Config.GridSize; x++)
            for (int y = 0; y < Config.GridSize; y++)
                for (int z = 0; z < Config.GridSize; z++)
                    InitializeGridCell(x, y, z);

        for (int i = 0; i < Config.InitialChildrenCnt; i++) ;
    }

    private void InitializeGridCell(int x, int y, int z)
    {
        _gridCells[x, y, z] = new GridCell(x, y, z);
    }

    public static bool IsPositionValid(int[] position)
    {
        return (0 <= position[0] && position[0] < Config.GridSize && 
                0 <= position[1] && position[1] < Config.GridSize && 
                0 <= position[2] && position[2] < Config.GridSize);
    }
}