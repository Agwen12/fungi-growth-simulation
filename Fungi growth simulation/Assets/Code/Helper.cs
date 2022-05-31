using System;

public class Helper
{
    public static Random Rnd = new Random(Config.RandomSeed);

    public static String GetTimestamp(DateTime value)
    {
        return value.ToString("yyyyMMddHHmmssffff");
    }
}
