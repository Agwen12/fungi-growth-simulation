using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Overlord : MonoBehaviour
{
    private Grid _grid;

    // Start is called before the first frame update
    void Start()
    {
        _grid = new Grid();
    }

    // Update is called once per frame
    void Update()
    {
        _grid.Update();
    }
}