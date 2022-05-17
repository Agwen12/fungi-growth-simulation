using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Config : MonoBehaviour
{
    public static float ChangeDirectionProbability = 0.04f;
    public static float BranchingFactor = 0.02f;
    public static int RandomSeed = 2137;
    public static float InitialChildrenPerc = 0.5f;
    public static int GridSize = 10;
    public static int[] MushroomCorePosition = { GridSize / 2, GridSize / 2, GridSize / 2 }; 
    public static float delta_x = 1e-6;
    public static float delta_t = 1;
    public static float si0 = 1e-06;
    public static float se0 = 1e-06;
    public static float v = 1e-02;
    public static float Dp = 1e-03;

    public static float Da = 3.456;
    public static float Di = 0.3456;

    public static float De = 0.3456;
    public static float b = 1e+06;
    public static float di = 1e-02;
    public static float c1 = 9e+07;
    public static float c2 = 1e-07;
    public static float c3 = 1e+08;
    public static float c4 = 1e-11;
    public static float c5 = 1e-11;

    public static float[] LayersOffsetsPerc = new float[] { 0.4f, 0.4f, 0f };

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
