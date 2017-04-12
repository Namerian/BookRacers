using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class BookUpgrade
{
    [SerializeField]
    private EVehicleStat _stat;

    [SerializeField]
    private float _valuePerLevel;

    [SerializeField]
    private int _numLevels;

    [SerializeField]
    private int _levelExpCost;

    [SerializeField]
    private int _levelMoneyCost;

    [SerializeField]
    private int _currentLevel;

    public EVehicleStat Stat { get { return _stat; } }

    public float ValuePerLevel { get { return _valuePerLevel; } }

    public int NumLevels { get { return _numLevels; } }

    public int LevelExpCost { get { return _levelExpCost; } }

    public int LevelMoneyCost { get { return _levelMoneyCost; } }

    public int CurrentLevel
    {
        get { return _currentLevel; }
        set
        {
            if (value < 0)
                _currentLevel = 0;
            else if (value > _numLevels)
                _currentLevel = _numLevels;
            else
                _currentLevel = value;
        }
    }
}
