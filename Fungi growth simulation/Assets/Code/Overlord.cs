using UnityEngine;

public class Overlord : MonoBehaviour
{
    public static int Frame = 0;

    private bool _startFinished = false;
    private Grid _grid;
    private bool paused = false;

    // Start is called before the first frame update
    void Start()
    {
        Config.Init();
        Statistics.Init();
        _grid = new Grid();
        Debug.Log("START");
        _startFinished = true;
    }
    
    public bool IsPaused()
    {
        return paused;
    }

    // Update is called once per frame
    void Update()
    {
        if (_startFinished)
        {
            if (Input.GetKeyDown(KeyCode.P))
                paused = !paused;

            if (!paused)
            {
                GridCell.MaxNutritionLevelCurr = 0;
                _grid.Update();
                Statistics.IncreaseTime();
                GridCell.MaxNutritionLevelPrev = GridCell.MaxNutritionLevelCurr;
                Debug.Log("FRAME " + Frame);
                Frame++;
            }
        }
    }
}
