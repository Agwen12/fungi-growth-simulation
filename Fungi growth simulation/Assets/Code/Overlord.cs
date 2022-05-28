using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Overlord : MonoBehaviour
{
    private Grid _grid;
    private int _frame = 0;

    // Start is called before the first frame update
    void Start()
    {
        Statistics.Init();
        _grid = new Grid();
    }

    // Update is called once per frame
    void Update()
    {
        _grid.Update();
        _frame++;
        Debug.Log("FRAME " + _frame);
    }
}
