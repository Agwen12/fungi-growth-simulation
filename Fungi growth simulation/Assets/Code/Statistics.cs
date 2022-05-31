using System;
using System.IO;

public static class Statistics
{
    private static int _time = 0;
    private static double _externalNutrition;
    private static double _internalNutrition;
    private static int _activeHyphal;
    private static int _tip;
    private static String _logFile = Helper.GetTimestamp(DateTime.Now) + ".csv";

    public static void Init()
    {
        Directory.CreateDirectory(Config.LogDirPath);
        using (StreamWriter sw = File.AppendText(Path.Combine(Config.LogDirPath, _logFile)))
        {
            sw.WriteLine("Time,External Nutrition,Internal Nutrition,Active Hyphal,Tip");
        }
        Reset();
    }

    private static void LogState()
    {
        using (StreamWriter sw = File.AppendText(Path.Combine(Config.LogDirPath, _logFile)))
        {
            sw.WriteLine($"{_time*Config.delta_t},{_externalNutrition},{_internalNutrition},{_activeHyphal},{_tip}");
        }
    }

    public static void IncreaseTime()
    {
        LogState();
        _time++;
        Reset();
    }

    public static void IncreaseExternalNutrition(double value)
    {
        _externalNutrition += value;
    }

    public static void IncreaseInternalNutrition(double value)
    {
        _internalNutrition += value;
    }

    public static void IncreaseHyphaCount(GridState hyphaType)
    {
        switch (hyphaType)
        {
            case GridState.ACTIVE_HYPHAL:
                _activeHyphal++;
                break;
            case GridState.TIP:
                _tip++;
                break;
        }
    }

    private static void Reset()
    {
        _externalNutrition = 0;
        _internalNutrition = 0;
        _activeHyphal = 0;
        _tip = 0;
    }
}
