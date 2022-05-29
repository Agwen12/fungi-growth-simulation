using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class Config : MonoBehaviour
{
    private static string filePath = "config.txt";

    public static void Init(){
        var dic = File.ReadAllLines(filePath).Select(l => l.Split(new[] { '=' })).ToDictionary( s => s[0].Trim(), s => s[1].Trim());
        RandomSeed = int.Parse(dic["RandomSeed"]);
        InitialChildrenPerc = float.Parse(dic["InitialChildrenPerc"]);
        GridSize = int.Parse(dic["GridSize"]);
        delta_x = double.Parse(dic["delta_x"], System.Globalization.NumberStyles.Float);
        delta_t = double.Parse(dic["delta_t"], System.Globalization.NumberStyles.Float);
        si0 = double.Parse(dic["si0"], System.Globalization.NumberStyles.Float);
        se0 = double.Parse(dic["se0"], System.Globalization.NumberStyles.Float);
        v = double.Parse(dic["v"], System.Globalization.NumberStyles.Float);
        Dp = double.Parse(dic["Dp"], System.Globalization.NumberStyles.Float);
        Da = double.Parse(dic["Da"], System.Globalization.NumberStyles.Float);
        Di = double.Parse(dic["Di"], System.Globalization.NumberStyles.Float);
        De = double.Parse(dic["De"], System.Globalization.NumberStyles.Float);
        b = double.Parse(dic["b"], System.Globalization.NumberStyles.Float);
        di = double.Parse(dic["di"], System.Globalization.NumberStyles.Float);
        c1 = double.Parse(dic["c1"], System.Globalization.NumberStyles.Float);
        c2 = double.Parse(dic["c2"], System.Globalization.NumberStyles.Float);
        c3 = double.Parse(dic["c3"], System.Globalization.NumberStyles.Float);
        c4 = double.Parse(dic["c4"], System.Globalization.NumberStyles.Float);
        c5 = double.Parse(dic["c5"], System.Globalization.NumberStyles.Float);
        LayersOffsetsPerc[0] = float.Parse(dic["LayersOffsetsPerc0"]);
        LayersOffsetsPerc[1] = float.Parse(dic["LayersOffsetsPerc1"]);
        LayersOffsetsPerc[2] = float.Parse(dic["LayersOffsetsPerc2"]);
        activeHyphaLifespan = int.Parse(dic["activeHyphaLifespan"]);
        MinCellColorV = float.Parse(dic["MinCellColorV"]);
    }

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
