using System;
using System.Collections;
using System.Collections.Generic;

public class Helper
{
    public static Random Rnd = new Random(Config.RandomSeed);

    // https://stackoverflow.com/questions/218060/random-gaussian-variables
    public static float SampleFromNormalDistribution(double mean, double stdDev)
    {
        System.Random rand = new System.Random(); //reuse this if you are generating many
        double u1 = 1.0 - rand.NextDouble(); //uniform(0,1] random doubles
        double u2 = 1.0 - rand.NextDouble();
        double randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) *
                               Math.Sin(2.0 * Math.PI * u2); //random normal(0,1)
        double randNormal = mean + stdDev * randStdNormal; //random normal(mean,stdDev^2)
        return (float)randNormal;
    }
}
