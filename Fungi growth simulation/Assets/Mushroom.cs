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

    public GameObject _gameObject { get; set; }
    public IMushroom _parent { get; set; }
    public Direction _direction { get; set; }

    public static Mushroom CreateComponent(GameObject gameObject, IMushroom parent, Direction direction)
    {
        Mushroom mushroom = gameObject.AddComponent<Mushroom>();
        mushroom._gameObject = gameObject;
        mushroom._parent = parent;
        mushroom._direction = direction;
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
            Vector3 parentPosition = _parent == null ? new Vector3(0, 0, 0) : _parent._gameObject.transform.position;
            Vector3 currPosition = _gameObject.transform.position;
            Vector3 direction = currPosition - parentPosition;

            if (Helper.Rnd.NextDouble() < 0.01f)
            { //Branch
                Vector3[] branchDirections = DirectionMethods.GetBranchDirection(_direction);
                Vector3 childPosition1 = currPosition + branchDirections[0];
                Vector3 childPosition2 = currPosition + branchDirections[1];

                Mushroom child1 = MushroomCore.SpawnMushroom(childPosition1, this, _direction);
                Mushroom child2 = MushroomCore.SpawnMushroom(childPosition2, this, _direction);

            } else {
              //Standart growth
                float angle = Helper.SampleFromNormalDistribution(56, 17);
                // Decide if we change direction
                if (Helper.Rnd.NextDouble() < 0.12f)
                    _direction = DirectionMethods.GetRandomDirection(_direction);
                Quaternion rotation = Quaternion.AngleAxis(angle, DirectionMethods.ToVector3(_direction));

                Vector3 childPosition = currPosition + rotation * direction;
                Mushroom child = MushroomCore.SpawnMushroom(childPosition, this, _direction);
                
            }
            _state = MushroomState.Hyphal;
        }
    }
}
