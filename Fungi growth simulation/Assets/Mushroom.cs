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

            if (Helper.Rnd.NextDouble() < 0.03f)
            { //Branch
                Vector3[] branchDirections = DirectionMethods.GetBranchDirection(Direction);
                Vector3 childPosition1 = currPosition + branchDirections[0].normalized;
                Vector3 childPosition2 = currPosition + branchDirections[1].normalized;
                Direction d1 = DirectionMethods.FromVector3(branchDirections[0]);
                Direction d2 = DirectionMethods.FromVector3(branchDirections[1]);
                Mushroom child1 = MushroomCore.SpawnMushroom(childPosition1, this, d1);
                Mushroom child2 = MushroomCore.SpawnMushroom(childPosition2, this, d2);
            } else {
              //Standart growth
                float angle = Helper.SampleFromNormalDistribution(56, 17);
                // Decide if we change direction
                if (Helper.Rnd.NextDouble() < 0.04f)
                    Direction = DirectionMethods.GetRandomPossibleDirection(Direction);
                Quaternion rotation = Quaternion.AngleAxis(angle, DirectionMethods.ToVector3(Direction));

                Vector3 childPosition = currPosition + rotation * direction.normalized;
                Mushroom child = MushroomCore.SpawnMushroom(childPosition, this, Direction);
                
            }
            _state = MushroomState.Hyphal;
        }
    }
}
