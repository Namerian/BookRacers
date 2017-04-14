using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[Serializable]
public class PilotData
{
    [Header("Constant Data")]

    [SerializeField]
    private string _name;

    [SerializeField]
    private int _cost;

    [SerializeField]
    private float _baseAcceleration;

    [SerializeField]
    private float _baseTurnSpeed;

    [SerializeField]
    private float _baseMass;

    [Header("Persistent Data")]

    [SerializeField]
    private int _experience;

    [SerializeField]
    private PilotUpgrade _upgrade;

    [SerializeField]
    private bool _unlocked;

    //=======================================================================================
    //
    //=======================================================================================

    public string Name { get { return _name; } private set { _name = value; } }

    public int Cost { get { return _cost; } }

    public float Acceleration
    {
        get
        {
            float value = _baseAcceleration;

            if (_upgrade.Stat == EVehicleStat.acceleration)
                value += _upgrade.CurrentLevel * _upgrade.ValuePerLevel;

            return value;
        }
    }

    public float TurnSpeed
    {
        get
        {
            float value = _baseTurnSpeed;

            if (_upgrade.Stat == EVehicleStat.turnSpeed)
                value += _upgrade.CurrentLevel * _upgrade.ValuePerLevel;

            return value;
        }
    }

    public float Mass
    {
        get
        {
            float value = _baseMass;

            if (_upgrade.Stat == EVehicleStat.mass)
                value += _upgrade.CurrentLevel * _upgrade.ValuePerLevel;

            return value;
        }
    }

    public int Experience { get { return _experience; } set { _experience = value; } }

    public PilotUpgrade Upgrade { get { return _upgrade; } private set { _upgrade = value; } }

    public bool Unlocked { get { return _unlocked; } set { _unlocked = value; } }

    //=======================================================================================
    //
    //=======================================================================================

    public void Load()
    {
        if (File.Exists(GameController.DATAPATH + "/Pilot_" + _name + ".dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs = File.Open(GameController.DATAPATH + "/Pilot_" + _name + ".dat", FileMode.Open);

            PilotData data = (PilotData)bf.Deserialize(fs);
            fs.Close();

            Experience = data.Experience;
            Upgrade = data.Upgrade;
            Unlocked = data.Unlocked;
        }
    }

    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream fs = File.Create(GameController.DATAPATH + "/Pilot_" + _name + ".dat");

        bf.Serialize(fs, this);
        fs.Close();
    }
}
