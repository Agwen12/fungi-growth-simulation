using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomCore : MonoBehaviour, IMushroom
{
    private static LinkedList<Mushroom> _mushrooms = new LinkedList<Mushroom>();

    public GameObject GameObject { get; set; }
    public IMushroom Parent { get; set; }

    public static GameObject MushroomPrefab;
    public int InitChildrenCount;
    public float InitRadius;
    public Vector3 InitPosition;

    // Start is called before the first frame update
    void Start()
    {
        Parent = null;
        MushroomPrefab = (GameObject)Instantiate(Resources.Load("Mushroom"));
        GameObject = GameObject.Find("MushroomCore");
        GameObject.transform.position = InitPosition;
        for (int i = 0; i < InitChildrenCount; i++)
            SpawnMushroom(Random.onUnitSphere * InitRadius, (IMushroom)this, DirectionMethods.GetRandomDirection());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static Mushroom SpawnMushroom(Vector3 position, IMushroom parent, Direction direction)
    {
        GameObject mushroomGameObject = Instantiate(MushroomPrefab, position, Quaternion.identity);
        Mushroom mushroom = Mushroom.CreateComponent(mushroomGameObject, parent, direction);
        _mushrooms.AddLast(mushroom);
        return mushroom;
    }
}
