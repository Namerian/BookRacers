﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[Serializable]
public class PilotData
{
    [SerializeField]
    private string _name;

    [SerializeField]
    private int _experience;

    public string Name { get { return _name; } private set { _name = value; } }

    public int Experience { get { return _experience; } private set { _experience = value; } }

    public void Load()
    {
        if (File.Exists(GameController.DATAPATH + "/Pilot_" + _name + ".dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs = File.Open(GameController.DATAPATH + "/Pilot_" + _name + ".dat", FileMode.Open);

            PilotData data = (PilotData)bf.Deserialize(fs);
            fs.Close();

            Name = data.Name;
            Experience = data.Experience;
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