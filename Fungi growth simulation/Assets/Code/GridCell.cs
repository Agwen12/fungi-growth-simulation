using System.Collections.Generic;
using System;
using UnityEngine;

public class GridCell
{
    public static double MaxNutritionLevelPrev = 0;
    public static double MaxNutritionLevelCurr = 0;

    private static double _maxExternalNutritionLevel = FindMaxExternalNutritionLevel();
    private static GameObject _prefab = Resources.Load("GridCell") as GameObject;
    private static Vector3 _prefabSize = (_prefab.GetComponent(typeof(Renderer)) as Renderer).bounds.size;

    public Direction GrowthDirection;
    public double NutritionLevel = Config.si0;
    public double ExternalNutritionLevel = Config.se0;

    private bool _shouldBeHandled = false;
    private GridState _state;
    private int _x;
    private int _y;
    private int _z;
    private Dictionary<Direction, GridCell> _neighbors = new Dictionary<Direction, GridCell>();
    private GameObject _gameObject;
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
            ExternalNutritionLevel *= Config.AbnormalNutritionSpots[coordsTuple];
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
    
    private bool CheckAndApplyMoveCost()
    {
        double cost = Config.delta_x * Config.c2;
        if (NutritionLevel >= cost)
        {
            NutritionLevel -= cost;
            return true;
        }
        return false;
    }

    private void Move()
    {
        double dtdx = Config.delta_t / Config.delta_x;
        double sameDirection = NutritionLevel * (Config.v * dtdx + Config.Dp * dtdx / Config.delta_x);
        double acuteAngle = Config.Dp * NutritionLevel * dtdx / Config.delta_x;
        double randomNumber = Helper.Rnd.NextDouble();

        if (randomNumber <= sameDirection)
        {
            if (_neighbors.ContainsKey(GrowthDirection) &&
                CheckAndApplyMoveCost())
            {
                _state = GridState.ACTIVE_HYPHAL;
                _neighbors[GrowthDirection].GrowthDirection = GrowthDirection;
                _neighbors[GrowthDirection].SetState(GridState.TIP);
                this.SetState(GridState.ACTIVE_HYPHAL);
            }
        }
        else if (sameDirection < randomNumber && randomNumber < sameDirection + acuteAngle)
        {
            // move 
            Direction newGrowthDirection = DirectionMethods.GetAcute(GrowthDirection);
            if (_neighbors.ContainsKey(newGrowthDirection) &&
                CheckAndApplyMoveCost())
            {
                _state = GridState.ACTIVE_HYPHAL;
                _neighbors[newGrowthDirection].GrowthDirection = GrowthDirection;
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
        double branchingProbabilty = Config.b * NutritionLevel * Config.delta_t;

        if (Helper.Rnd.NextDouble() <= branchingProbabilty) 
        {
            Direction newGrowthDirection = DirectionMethods.GetAcute(GrowthDirection);
            if (_neighbors.ContainsKey(newGrowthDirection) && 
                _neighbors[newGrowthDirection]._state == GridState.EMPTY &&
                CheckAndApplyMoveCost())
            {
                _neighbors[newGrowthDirection].GrowthDirection = newGrowthDirection;
                _neighbors[newGrowthDirection].SetState(GridState.TIP);
            }
        }
    }

    private void PassiveSubstanceMovement()
    {
        foreach (KeyValuePair<Direction, GridCell> entry in _neighbors)
        {
            if ((entry.Value._state == GridState.ACTIVE_HYPHAL))
                {
                    // nutrines k ====> j
                    double ammount = Config.Di * Config.delta_t *
                                     (NutritionLevel - entry.Value.NutritionLevel) / (Config.delta_x * Config.delta_x);

                    if (ammount > 0 && entry.Value.NutritionLevel >= ammount)
                    {
                        //Debug.Log("NUTRINES k =====> j " + ammount);
                        NutritionLevel += ammount;
                        entry.Value.NutritionLevel -= ammount;
                    }
                }
        }
    }

    private void Uptake()
    {
        double ammount;
        foreach (var neighborCell in _neighbors.Values)
        {
            ammount = neighborCell.ExternalNutritionLevel * NutritionLevel * Config.delta_t;
            if (neighborCell._state == GridState.EMPTY &&
                neighborCell.ExternalNutritionLevel > Config.c3 * ammount)
            {
                NutritionLevel += Config.c1 * ammount;
                neighborCell.ExternalNutritionLevel -= Config.c3 * ammount;
            }
        }

        ammount = ExternalNutritionLevel * NutritionLevel * Config.delta_t;
        if (_state == GridState.EMPTY &&
            ExternalNutritionLevel > Config.c3 * ammount)
        {
            NutritionLevel += Config.c1 * ammount;
            ExternalNutritionLevel -= Config.c3 * ammount;
        }
    }

    private void AdjustColor()
    {
        float H, S, V;
        Color colorBase = GridStateMethods.ToColor(_state);
        Color.RGBToHSV(colorBase, out H, out S, out V);
        V = Math.Max((float)(NutritionLevel / MaxNutritionLevelPrev), Config.MinCellColorV);
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
            if (_maxExternalNutritionLevel > 0)
                scalar *= (float)(ExternalNutritionLevel / _maxExternalNutritionLevel);
            newSize *= scalar;
        }

        _gameObject.transform.localScale = newSize;
    }

    private void GrowOld()
    {
        if (_state == GridState.ACTIVE_HYPHAL)
            ++_age;
    }

    public void Update()
    {
        Statistics.IncreaseInternalNutrition(NutritionLevel);
        Statistics.IncreaseExternalNutrition(ExternalNutritionLevel);
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

            if (NutritionLevel > MaxNutritionLevelCurr)
                MaxNutritionLevelCurr = NutritionLevel;

            AdjustColor();
            AdjustSize();

            switch (_state)
            {
                case GridState.ACTIVE_HYPHAL:
                    this.Uptake();
                    this.Branch();
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
