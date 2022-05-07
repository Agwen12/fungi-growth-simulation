using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomCore : MonoBehaviour
{
    private static LinkedList<Mushroom> _mushrooms = new LinkedList<Mushroom>();
    private GameObject _gameObject;

    public static GameObject MushroomPrefab;
    public int InitChildrenCount;
    public float InitRadius;
    public Vector3 InitPosition;

    // Start is called before the first frame update
    void Start()
    {
        MushroomPrefab = (GameObject)Instantiate(Resources.Load("Mushroom"));
        _gameObject = GameObject.Find("MushroomCore");
        _gameObject.transform.position = InitPosition;
        for (int i = 0; i < InitChildrenCount; i++)
        {
            SpawnMushroom(Random.onUnitSphere * InitRadius, _gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static Mushroom SpawnMushroom(Vector3 position, GameObject parent)
    {
        GameObject mushroomGameObject = Instantiate(MushroomPrefab, position, Quaternion.identity);
        Mushroom mushroom = Mushroom.CreateComponent(mushroomGameObject, parent);
        _mushrooms.AddLast(mushroom);
        return mushroom;
    }
}
