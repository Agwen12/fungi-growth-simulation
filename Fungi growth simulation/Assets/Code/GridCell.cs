using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class GridCell
{
    private Dictionary<Direction, GridCell> _neighbors = new Dictionary<Direction, GridCell>();
    private GridState _state;
    public Direction _growthDirection;
    public double _nutritionLevel = Config.si0;

    public double _externalNutritionLevel = Config.se0;

    private int _x;
    private int _y;
    private int _z;
    private GameObject _gameObject;
    private static GameObject _prefab = GameObject.Instantiate(Resources.Load("GridCell")) as GameObject;
    private static Vector3 _prefabSize = (_prefab.GetComponent(typeof(Renderer)) as Renderer).bounds.size;

    public GridCell(int x, int y, int z)
    {
        _state = GridState.EMPTY;
        _x = x;
        _y = y;
        _z = z;
        Vector3 position = new Vector3(_x * _prefabSize.x, _y * _prefabSize.y, _z * _prefabSize.z);
        if (z % 2 == 1)
        {
            position += new Vector3(Config.LayersOffsetsPerc[0] * _prefabSize.x,
                                    Config.LayersOffsetsPerc[1] * _prefabSize.y,
                                    Config.LayersOffsetsPerc[2] * _prefabSize.z);
        }
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
        double dtdx = Config.delta_t / Config.delta_x;
        double sameDirection = _nutritionLevel * (Config.v * dtdx + Config.Dp * dtdx / Config.delta_x);
        double acuteAngle = Config.Dp * _nutritionLevel * dtdx / Config.delta_x;
        double randomNumber = Helper.Rnd.NextDouble();
        /*Debug.Log("SAME DIR " + sameDirection + " MOVE " + acuteAngle);*/
        if (randomNumber <= sameDirection) 
        {
            if (_neighbors.ContainsKey(_growthDirection))
            {
                _state = GridState.ACTIVE_HYPHAL;
                _neighbors[_growthDirection]._growthDirection = _growthDirection;
                _neighbors[_growthDirection].SetState(GridState.TIP);
                this.SetState(GridState.ACTIVE_HYPHAL);
            }
        }
        else if (sameDirection < randomNumber && randomNumber < sameDirection + acuteAngle)
        {
            // move 
            Direction newGrowthDirection = DirectionMethods.GetAcute(_growthDirection);
            if (_neighbors.ContainsKey(newGrowthDirection))
            {
                _state = GridState.ACTIVE_HYPHAL;
                _neighbors[newGrowthDirection]._growthDirection = _growthDirection;
                _neighbors[newGrowthDirection].SetState(GridState.TIP);
                this.SetState(GridState.ACTIVE_HYPHAL);
            }

        } 
        else 
        {
            // don't move
        }
    }

    private void Branch()
    {
        double branchingProbabilty = Config.b * _nutritionLevel * Config.delta_t;
        Debug.Log("BRANCHING " + branchingProbabilty);
        if (Helper.Rnd.NextDouble() <= branchingProbabilty) 
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
            if ((entry.Value._state == GridState.ACTIVE_HYPHAL ||
                entry.Value._state == GridState.INACTIVE_HYPHAL ||
                entry.Value._state == GridState.TIP) )
                {
                    // nutrines k ====> j
                    double ammount = Config.Da * Config.delta_t *
                                     (entry.Value._nutritionLevel - _nutritionLevel) / (Config.delta_x * Config.delta_x);

                    _nutritionLevel += ammount;
                    entry.Value._nutritionLevel -= ammount;
                }
        }
    }

    private void Uptake()
    {
        if (_externalNutritionLevel > 0) 
        {
            double ammount = _externalNutritionLevel * _nutritionLevel * Config.delta_t;
            _nutritionLevel += Config.c1 * ammount;
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
        _gameObject.GetComponent<Renderer>()
                   .material
                   .SetColor("_Color", GridStateMethods.toColor(_state));
        _gameObject.SetActive(_state != GridState.EMPTY);
/*        if (_state != GridState.EMPTY)
            Debug.Log("ENERGIA " + _nutritionLevel);*/
    }
}
