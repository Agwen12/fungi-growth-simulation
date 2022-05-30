using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid
{
    private GridCellsWrapper _gridCells;

    public Grid()
    {
        _gridCells = new GridCellsWrapper(Config.GridSize[0], Config.GridSize[1], Config.GridSize[2]);

        for (int x = 0; x < Config.GridSize[0]; x++)
            for (int y = 0; y < Config.GridSize[1]; y++)
                for (int z = 0; z < Config.GridSize[2]; z++)
                    InitializeGridCell(x, y, z);

        for (int x = 0; x < Config.GridSize[0]; x++)      
            for (int y = 0; y < Config.GridSize[1]; y++)        
                for (int z = 0; z < Config.GridSize[2]; z++)
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
                    _gridCells[neighborPosition].SetState(GridState.TIP);
                _gridCells[neighborPosition]._growthDirection = direction;
            }
            ++i;
        }
        _gridCells[Config.MushroomCorePosition].SetState(GridState.ACTIVE_HYPHAL);
    }

    public static bool IsPositionValid(int[] position)
    {
        return (0 <= position[0] && position[0] < Config.GridSize[0] && 
                0 <= position[1] && position[1] < Config.GridSize[1] && 
                0 <= position[2] && position[2] < Config.GridSize[2]);
    }

    public void Update()
    {
        for (int x = 0; x < Config.GridSize[0]; x++)
            for (int y = 0; y < Config.GridSize[1]; y++)
                for (int z = 0; z < Config.GridSize[2]; z++)
                    _gridCells[x, y, z].Update();
    }
}
