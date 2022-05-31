using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class Config : MonoBehaviour
{
    private static string configPath = "config.txt";
    private static string abnormalSpotsPath = "abnormal_nutrition_spots.txt";

    public static void Init(){
        var dic = File.ReadAllLines(configPath).Select(l => l.Split(new[] { '=' })).ToDictionary( s => s[0].Trim(), s => s[1].Trim());
        RandomSeed = int.Parse(dic["RandomSeed"]);
        InitialChildrenPerc = float.Parse(dic["InitialChildrenPerc"]);
        GridSize[0] = int.Parse(dic["GridSize0"]);
        GridSize[1] = int.Parse(dic["GridSize1"]);
        GridSize[2] = int.Parse(dic["GridSize2"]);
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

        var dic2 = File.ReadAllLines(abnormalSpotsPath).Select(l => l.Split(new[] { ')' })).ToArray();
        string[] coords;
        int[] coordsInt;
        double val;
        AbnormalNutritionSpots = new Dictionary<Tuple<int, int, int>, double>();
        foreach (string[] el in dic2){
            coords = el[0].Trim(new[]{'('}).Split(new[]{' '});
            coordsInt = Array.ConvertAll(coords, int.Parse);
            val = double.Parse(el[1]);
            AbnormalNutritionSpots.Add(new Tuple<int, int, int>(coordsInt[0], coordsInt[1], coordsInt[2]), val);
        }
    }

    public static int RandomSeed = 2137;
    public static float InitialChildrenPerc = 1.0f;
    public static int[] GridSize = { 20, 20, 40 };
    public static int[] MushroomCorePosition = { GridSize[0] / 2, GridSize[1] / 2, GridSize[2] / 2 };
    public static double delta_x = 2e-5;
    public static double delta_t = 1e-2;
    public static double si0 = 1e-06;
    public static double se0 = 1e-06;
    public static double v = 1e-02;
    public static double Dp = 1e-03;
    public static double Da = 3.456;
    public static double Di = 0.3456;
    public static double De = 0.3456;
    public static double b = 1e+05;
    public static double di = 1e-02;
    public static double c1 = 9e+07;
    public static double c2 = 1e-07;
    public static double c3 = 1e+08;
    public static double c4 = 1e-11;
    public static double c5 = 1e-11;

    public static float[] LayersOffsetsPerc = new float[] { 0.5f, 0.5f, 0.7f };

    public static int activeHyphaLifespan = 10000000;

    public static string LogDirPath = "log";
    public static float MinCellColorV = 0.25f;

    public static Dictionary<Tuple<int, int, int>, double> AbnormalNutritionSpots = new Dictionary<Tuple<int, int, int>, double>()
    {
        { new Tuple<int, int, int>(7, 7, 7), 100.0f },
        { new Tuple<int, int, int>(8, 8, 8), 90.0f },
        { new Tuple<int, int, int>(9, 9, 9), 100.0f },
        { new Tuple<int, int, int>(10, 10, 10), 125.0f },
        { new Tuple<int, int, int>(7, 8, 7), 110.0f },
        { new Tuple<int, int, int>(8, 8, 9), 110.0f },
        { new Tuple<int, int, int>(9, 10, 9), 120.0f },
        { new Tuple<int, int, int>(10, 11, 10), 120.0f },
    };

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
