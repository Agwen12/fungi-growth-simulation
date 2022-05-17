using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid
{
    private GridCellsWrapper _gridCells;

    public Grid()
    {
        _gridCells = new GridCellsWrapper(Config.GridSize, Config.GridSize, Config.GridSize);

        for (int x = 0; x < Config.GridSize; x++)
            for (int y = 0; y < Config.GridSize; y++)
                for (int z = 0; z < Config.GridSize; z++)
                    InitializeGridCell(x, y, z);

        for (int x = 0; x < Config.GridSize; x++)      
            for (int y = 0; y < Config.GridSize; y++)        
                for (int z = 0; z < Config.GridSize; z++)
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
                _gridCells[x, y, z].AddNeighbor(direction, _gridCells[offsetPosition]);
        }
    }

    private void InitializeMushroomCore()
    {
        int i = 0;
        int gap = Config.InitialChildrenPerc == 0 ? int.MaxValue : (int)(1 / Config.InitialChildrenPerc);
        foreach (Direction direction in Enum.GetValues(typeof(Direction)))
        {
            if (i % gap == 0)
            {
                int[] neighborPosition = DirectionMethods.GetOffsetPosition(Config.MushroomCorePosition, direction);
                if (IsPositionValid(neighborPosition))
                    _gridCells[neighborPosition].SetState(GridState.ACTIVE_HYPHAL);
            }
            ++i;
        }
    }

    public static bool IsPositionValid(int[] position)
    {
        return (0 <= position[0] && position[0] < Config.GridSize && 
                0 <= position[1] && position[1] < Config.GridSize && 
                0 <= position[2] && position[2] < Config.GridSize);
    }

    public void Update()
    {
        for (int x = 0; x < Config.GridSize; x++)
            for (int y = 0; y < Config.GridSize; y++)
                for (int z = 0; z < Config.GridSize; z++)
                    _gridCells[x, y, z].Update();
    }
}
