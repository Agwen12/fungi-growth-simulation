using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Overlord : MonoBehaviour
{
    private Grid _grid;
    public static int Frame = 0;

    // Start is called before the first frame update
    void Start()
    {
        Statistics.Init();
        _grid = new Grid();
    }

    // Update is called once per frame
    void Update()
    {
        GridCell._maxNutritionLevelCurr = 0;
        _grid.Update();
        Statistics.IncreaseTime();
        GridCell._maxNutritionLevelPrev = GridCell._maxNutritionLevelCurr;

        Debug.Log("FRAME " + Frame);
        Frame++;
    }
}
