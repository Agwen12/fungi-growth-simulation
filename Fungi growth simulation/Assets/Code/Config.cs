using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class Config : MonoBehaviour
{
    private static string _configPath = "config.txt";
    private static string _abnormalSpotsPath = "abnormal_nutrition_spots.txt";

    public static void Init(){
        var dic = File.ReadAllLines(_configPath).Select(l => l.Split(new[] { '=' })).ToDictionary( s => s[0].Trim(), s => s[1].Trim());
        RandomSeed = int.Parse(dic["RandomSeed"]);
        InitialChildrenPerc = float.Parse(dic["InitialChildrenPerc"]);
        GridSize[0] = int.Parse(dic["GridSize0"]);
        GridSize[1] = int.Parse(dic["GridSize1"]);
        GridSize[2] = int.Parse(dic["GridSize2"]);
        MushroomCorePosition = new int[] { GridSize[0] / 2, GridSize[1] / 2, GridSize[2] / 2 };
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
        LogDirPath = dic["LogDirPath"];
        MinCellColorV = float.Parse(dic["MinCellColorV"]);

        var dic2 = File.ReadAllLines(_abnormalSpotsPath).Select(l => l.Split(new[] { ')' })).ToArray();
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

    public static int RandomSeed;
    public static float InitialChildrenPerc;
    public static int[] GridSize = new int[3];
    public static int[] MushroomCorePosition;
    public static double delta_x;
    public static double delta_t;
    public static double si0;
    public static double se0;
    public static double v;
    public static double Dp;
    public static double Da;
    public static double Di;
    public static double De;
    public static double b;
    public static double di;
    public static double c1;
    public static double c2;
    public static double c3;
    public static double c4;
    public static double c5;
    public static float[] LayersOffsetsPerc = new float[3];
    public static string LogDirPath;
    public static float MinCellColorV;
    public static Dictionary<Tuple<int, int, int>, double> AbnormalNutritionSpots;

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
