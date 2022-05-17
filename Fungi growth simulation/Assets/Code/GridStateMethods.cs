using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GridStateMethods
{
    public static Color toColor(GridState gridState)
    {
        return gridState switch
        {
            GridState.ACTIVE_HYPHAL => Color.green,
            GridState.INACTIVE_HYPHAL => Color.gray,
            GridState.TIP => Color.blue,
            GridState.EMPTY => Color.magenta,
            _ => Color.black
        };
    }
}
