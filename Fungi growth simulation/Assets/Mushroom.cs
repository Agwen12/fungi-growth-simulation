using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MushroomState
{
    Tip,
    Hyphal
}

public class Mushroom : MonoBehaviour, IMushroom
{
    private MushroomState _state = MushroomState.Tip;

    public GameObject GameObject { get; set; }
    public IMushroom Parent { get; set; }
    public Direction Direction { get; set; }

    public static Mushroom CreateComponent(GameObject gameObject, IMushroom parent, Direction direction)
    {
        Mushroom mushroom = gameObject.AddComponent<Mushroom>();
        mushroom.GameObject = gameObject;
        mushroom.Parent = parent;
        mushroom.Direction = direction;
        return mushroom;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (_state == MushroomState.Tip)
        {
            Vector3 parentPosition = Parent == null ? new Vector3(0, 0, 0) : Parent.GameObject.transform.position;
            Vector3 currPosition = GameObject.transform.position;
            Vector3 direction = currPosition - parentPosition;
            
            float angle = Helper.SampleFromNormalDistribution(56, 17);
            // Decide if we change direction
            if (Helper.Rnd.NextDouble() < 0.2f)
                Direction = DirectionMethods.GetRandomPossibleDirection(Direction);
            Quaternion rotation = Quaternion.AngleAxis(angle, DirectionMethods.ToVector3(Direction));

            Vector3 childPosition = currPosition + rotation * direction;
            Mushroom child = MushroomCore.SpawnMushroom(childPosition, this, Direction);
            _state = MushroomState.Hyphal;
        }
    }
}
