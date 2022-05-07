using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MushroomState
{
    Tip,
    Hyphal
}

public class Mushroom : MonoBehaviour
{
    private MushroomState _state = MushroomState.Tip;
    private GameObject _gameObject { get; set; }
    private GameObject _parent { get; set; }

    public static Mushroom CreateComponent(GameObject gameObject, GameObject parent = null)
    {
        Mushroom mushroom = gameObject.AddComponent<Mushroom>();
        mushroom._gameObject = gameObject;
        mushroom._parent = parent;
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
            Vector3 parentPosition = _parent == null ? new Vector3(0, 0, 0) : _parent.transform.position;
            Vector3 parentDirection = _gameObject.transform.position - parentPosition;
            Vector3 rotationAxis = parentDirection;
            Quaternion rotation = Quaternion.AngleAxis(Helper.sampleFromNormalDistribution(56, 17), rotationAxis);
            Vector3 childPosition = parentPosition + rotation * parentDirection;
            // childPosition == parentPosition for some reason, need to investigate that

            Mushroom child = MushroomCore.SpawnMushroom(childPosition, _parent);
            child._parent = _gameObject;
            _state = MushroomState.Hyphal;
        }
    }
}
