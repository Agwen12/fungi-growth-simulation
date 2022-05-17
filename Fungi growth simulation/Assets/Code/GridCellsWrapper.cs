using System.Collections;
using System.Collections.Generic;


public class GridCellsWrapper
{
    private GridCell[,,] _gridCells;

    public GridCellsWrapper(int xDim, int yDim, int zDim)
    {
        _gridCells = new GridCell[xDim, yDim, zDim];
    }

    public GridCell this[int x, int y, int z]
    {
        get => _gridCells[x, y, z];
        set => _gridCells[x, y, z] = value;
    }
    
    public GridCell this[int[] indices]
    {
        get => _gridCells[indices[0], indices[1], indices[2]];
        set => _gridCells[indices[0], indices[1], indices[2]] = value;
    }
}
