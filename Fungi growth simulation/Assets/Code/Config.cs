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
