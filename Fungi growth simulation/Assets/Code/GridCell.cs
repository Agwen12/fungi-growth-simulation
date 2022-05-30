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
    private static double _maxExternalNutritionLevel = FindMaxExternalNutritionLevel();
    private bool _shouldBeHandled = false;

    public double _externalNutritionLevel = Config.se0;
    public static double _maxNutritionLevelPrev = 0;
    public static double _maxNutritionLevelCurr = 0;

    private int _x;
    private int _y;
    private int _z;
    private GameObject _gameObject;
    private static GameObject _prefab = GameObject.Instantiate(Resources.Load("GridCell")) as GameObject;
    private static Vector3 _prefabSize = (_prefab.GetComponent(typeof(Renderer)) as Renderer).bounds.size;
    private int _age = 0;

    public GridCell(int x, int y, int z)
    {
        _state = GridState.EMPTY;
        _x = x;
        _y = y;
        _z = z;

        Vector3 position = new Vector3(_x * _prefabSize.x, _y * _prefabSize.y, _z * _prefabSize.z);
        Vector3 layerOffset = new Vector3(0, 0, Config.LayersOffsetsPerc[2] * _prefabSize.z * (-Config.LayersOffsetsPerc[2] * _z));
        if (z % 2 == 1)
        {
            position += new Vector3(Config.LayersOffsetsPerc[0] * _prefabSize.x,
                                    Config.LayersOffsetsPerc[1] * _prefabSize.y,
                                    Config.LayersOffsetsPerc[2] * _prefabSize.z);
        }
        else
            position += new Vector3(0, 0, Config.LayersOffsetsPerc[2] * _prefabSize.z);
        position += layerOffset;
        _gameObject = GameObject.Instantiate(_prefab, position, Quaternion.identity);

        Tuple<int, int, int> coordsTuple = new Tuple<int, int, int>(_x, _y, _z);
        if (Config.AbnormalNutritionSpots.ContainsKey(coordsTuple))
        {
            _externalNutritionLevel *= Config.AbnormalNutritionSpots[coordsTuple];
            _shouldBeHandled = true;
        }
    }

    public void AddNeighbor(Direction direction, GridCell neighbor)
    {
        _neighbors.Add(direction, neighbor);
    }

    public void SetState(GridState state)
    {
        _state = state;
    }

    private static double FindMaxExternalNutritionLevel()
    {
        double maxScalar = 0;

        foreach (var scalar in Config.AbnormalNutritionSpots.Values)
            maxScalar = Math.Max(maxScalar, scalar);

        return maxScalar * Config.se0;
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
        /*Debug.Log("BRANCHING " + branchingProbabilty);*/
        if (Helper.Rnd.NextDouble() <= branchingProbabilty) 
        {
            Direction newGrowthDirection = DirectionMethods.GetAcute(_growthDirection);
            if (_neighbors.ContainsKey(newGrowthDirection))
            {
                _neighbors[newGrowthDirection]._growthDirection = newGrowthDirection;
                _neighbors[newGrowthDirection].SetState(GridState.TIP);
            }
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
        double ammount = _externalNutritionLevel * _nutritionLevel * Config.delta_t;
        if (_externalNutritionLevel > Config.c3 * ammount) 
        {
            _nutritionLevel += Config.c1 * ammount;
            _externalNutritionLevel -= Config.c3 * ammount;
        }
    }

    private void AdjustColor()
    {
        float H, S, V;
        Color colorBase = GridStateMethods.toColor(_state);
        Color.RGBToHSV(colorBase, out H, out S, out V);
        V = Math.Min((float)(_nutritionLevel / _maxNutritionLevelPrev), Config.MinCellColorV);
        Color color = Color.HSVToRGB(H, S, V);

        _gameObject.GetComponent<Renderer>()
           .material
           .SetColor("_Color", color);
    }

    private void AdjustSize()
    {
        Vector3 newSize = _prefab.transform.localScale;

        if (_state == GridState.EMPTY)
        {
            float scalar = 0.75f;
            scalar *= (float)(_externalNutritionLevel / _maxExternalNutritionLevel);
            newSize *= scalar;
        }

        _gameObject.transform.localScale = newSize;
    }

    private void GrowOld()
    {
        if (_state == GridState.ACTIVE_HYPHAL)
            if (++_age >= Config.activeHyphaLifespan)
                _state = GridState.INACTIVE_HYPHAL;
    }

    public void Update()
    {
        Statistics.IncreaseInternalNutrition(_nutritionLevel);
        Statistics.IncreaseExternalNutrition(_externalNutritionLevel);
        Statistics.IncreaseHyphaCount(_state);

        if (Overlord.Frame == 0)
        {
            AdjustColor();
            AdjustSize();
            _gameObject.SetActive(_shouldBeHandled);
        }

        if (_shouldBeHandled || _state != GridState.EMPTY)
        {
            _shouldBeHandled = true;
            _gameObject.SetActive(true);

            if (_nutritionLevel > _maxNutritionLevelCurr)
                _maxNutritionLevelCurr = _nutritionLevel;

            AdjustColor();
            AdjustSize();

            switch (_state)
            {
                case GridState.ACTIVE_HYPHAL:
                    this.Uptake();
                    this.Branch();
                    this.PassiveSubstanceMovement();
                    break;
                case GridState.INACTIVE_HYPHAL:
                    this.Uptake();
                    this.PassiveSubstanceMovement();
                    break;
                case GridState.TIP:
                    this.Move();
                    break;
                default:
                    break;
            }

            GrowOld();
        }
    }
}
