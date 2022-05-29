using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Config : MonoBehaviour
{
    public static int RandomSeed = 2137;
    public static float InitialChildrenPerc = 1.0f;
    public static int GridSize = 25;
    public static int[] MushroomCorePosition = { GridSize / 2, GridSize / 2, GridSize / 2 }; 
    public static double delta_x = 1e-01;
    public static double delta_t = 1e-03;
    public static double si0 = 1e-06;
    public static double se0 = 1e+01;
    public static double v = 1e-02;
    public static double Dp = 1e-03;

    public static double Da = 3.456;
    public static double Di = 0.3456;

    public static double De = 0.3456;
    public static double b = 1e-02;
    public static double di = 1e-02;
    public static double c1 = 9e+01;
    public static double c2 = 1e-07;
    public static double c3 = 1e+01;
    public static double c4 = 1e-11;
    public static double c5 = 1e-11;

    public static float[] LayersOffsetsPerc = new float[] { 0.5f, 0.5f, 0.7f };

    public static int activeHyphaLifespan = 500;

    public static string LogDirPath = "log";
    public static float MinCellColorV = 0.33f;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 10;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
