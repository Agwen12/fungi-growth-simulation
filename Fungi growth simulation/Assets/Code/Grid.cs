using System;
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

        for (int x = 0; x < Config.GridSize; x++)      
            for (int y = 0; y < Config.GridSize; y++)        
                for (int z = 0; y < Config.GridSize; z++)
                    SetNeighbors(x, y, z);

        InitializeMushroomCore();
    }

    private void InitializeGridCell(int x, int y, int z)
    {
        _gridCells[x, y, z] = new GridCell(x, y, z);
    }

    private void SetNeighbors(int x, int y, int z) 
    {
        foreach (Direction direction in Enum.GetValues(typeof(Direction)))
        {
            int[] offsetPosition = DirectionMethods.GetOffsetPosition(new int[] { x, y, z }, direction);
            if (IsPositionValid(offsetPosition))
                _gridCells[x, y, z].AddNeighbor(direction, _gridCells[offsetPosition[0], offsetPosition[1], offsetPosition[2]]);
        }
    }

    private void InitializeMushroomCore()
    {
        GridCell mushroomCore = _gridCells[Config.MushroomCorePosition[0], Config.MushroomCorePosition[1], Config.MushroomCorePosition[2]];
        int i = 0;
        int gap = Config.InitialChildrenPerc == 0 ? int.MaxValue : (int)(1 / Config.InitialChildrenPerc);
        foreach (Direction direction in Enum.GetValues(typeof(Direction)))
        {
            if (i % gap == 0)
            {
                int[] neighborPosition = DirectionMethods.GetOffsetPosition(Config.MushroomCorePosition, direction);
                if (IsPositionValid(neighborPosition))
                    _gridCells[neighborPosition[0], neighborPosition[1], neighborPosition[2]].SetState(GridState.ACTIVE_HYPHAL);
            }
        }
    }

    public static bool IsPositionValid(int[] position)
    {
        return (0 <= position[0] && position[0] < Config.GridSize && 
                0 <= position[1] && position[1] < Config.GridSize && 
                0 <= position[2] && position[2] < Config.GridSize);
    }
}
