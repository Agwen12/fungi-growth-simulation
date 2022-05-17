using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Direction;


public class GridCell
{
    private Dictionary<Direction, GridCell> _neighbors = new Dictionary<Direction, GridCell>();
    private GridState _state;
    private Direction _growthDirection;
    public float _nutritionLevel; // perhabs consider using a getter

    public float _externalNutritionLevel;

    private int _x;
    private int _y;
    private int _z;
    private GameObject _gameObject;
    private static GameObject _prefab = GameObject.Instantiate(Resources.Load("GridCell")) as GameObject;
    private static Vector3 _prefabSize = (_prefab.GetComponent(typeof(Renderer)) as Renderer).bounds.size;

    public GridCell(int x, int y, int z)
    {
        _x = x;
        _y = y;
        _z = z;
        Vector3 position;
        if (z % 2 == 0)
            position = new Vector3(_x * _prefabSize.x, _y * _prefabSize.y, _z * _prefabSize.z);
        else
            position = new Vector3((_x + 0.5f) * _prefabSize.x, (_y + 0.5f) * _prefabSize.y, _z * _prefabSize.z);
        _gameObject = GameObject.Instantiate(_prefab, position, Quaternion.identity);
    }

    public void AddNeighbor(Direction direction, GridCell neighbor)
    {
        _neighbors.Add(direction, neighbor);
    }

    public void SetState(GridState state)
    {
        _state = state;
    }

    private void Move()
    {
        float dtdx = Config.delta_t / Config.delta_x;
        float sameDirection = _nutritionLevel* (Config.v *dtdx   + Config.Dp * dtdx / Config.delta_x);
        float acuteAngle = Config.Dp * _nutritionLevel * dtdx / Config.delta_x;
        float randomNumber = Helper.Rnd.NextDouble();
        if ( randomNumber <= sameDirection) 
        {
            _state = GridState.ACTIVE_HYPHAL;
            _neighbors[_growthDirection]._growthDirection = _growthDirection;
            _neighbors[_growthDirection].SetState(GridState.TIP);
            this.SetState(GridState.ACTIVE_HYPHAL);
        }
        else if (sameDirection < randomNumber < sameDirection + acuteAngle)
        {
            // move 
            Direction newGrowthDirection = DirectionMethods.GetAcute(_growthDirection);
            _state = GridState.ACTIVE_HYPHAL;
            _neighbors[newGrowthDirection]._growthDirection = _growthDirection;
            _neighbors[newGrowthDirection].SetState(GridState.TIP);
            this.SetState(GridState.ACTIVE_HYPHAL);
        } 
        else 
        {
            // don't move
        }
    }

    private void Branch()
    {
        float branchingProbabilty = Config.b * _nutritionLevel * Config.delta_t;
        if (Helper.Rnd.NextDouble <= branchingProbabilty) 
        {
            Direction newGrowthDirection = DirectionMethods.GetAcute(_growthDirection);
            _neighbors[newGrowthDirection]._growthDirection = newGrowthDirection;
            _neighbors[newGrowthDirection].SetState(GridState.TIP);
        }
    }

    private void PassiveSubstanceMovement()
    {
        foreach (KeyValuePair<Direction, GridCell> entry in _neighbors)
        {

            if ((entry.Value.GetType() == GridState.ACTIVE_HYPHAL ||
                entry.Value.GetType() == GridState.INACTIVE_HYPHAL ||
                entry.Value.GetType() == GridState.TIP) )
                {
                    // nutrines k ====> j
                    float ammount = Config.ChangeDirectionProbability * Config.delta_t *
                     (entry.Value._nutritionLevel - _nutritionLevel) / (Config.delta_x * Config.delta_x);

                    _nutritionLevel += ammount;
                    entry.Value._nutritionLevel -= ammount;
                }
        }
    }

    private void Uptake()
    {
        if(_externalNutritionLevel > 0) 
        {
            float ammount = _externalNutritionLevel * _nutritionLevel * Config.delta_t;
            _nutritionLevel += Confing.c1 * ammount;
            _externalNutritionLevel -= Config.c3 * ammount;
        }
        
    }

    public void Update()
    {
        switch (_state)
        {
            case GridState.ACTIVE_HYPHAL:
                // set color 1
                this.Uptake();
                this.Branch();
                this.PassiveSubstanceMovement();
                break;
            case GridState.INACTIVE_HYPHAL:
                // set color 2
                break;
            case GridState.TIP:
                // set color 3
                this.Move();
                break;
            default:
                break;
        }
    }
}
