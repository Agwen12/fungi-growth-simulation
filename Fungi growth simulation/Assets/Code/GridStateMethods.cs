using UnityEngine;

public static class GridStateMethods
{
    public static Color ToColor(GridState gridState)
    {
        return gridState switch
        {
            GridState.ACTIVE_HYPHAL => Color.green,
            GridState.TIP => Color.red,
            GridState.EMPTY => Color.magenta,
            _ => Color.black
        };
    }
}
