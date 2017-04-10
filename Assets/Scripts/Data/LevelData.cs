using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[Serializable]
public class LevelData
{
    [SerializeField]
    private string _name;

    [SerializeField]
    private string _sceneName;

    public string Name { get { return _name; } private set { _name = value; } }

    public void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/Level_" + _name + ".dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs = File.Open(Application.persistentDataPath + "/Level_" + _name + ".dat", FileMode.Open);

            LevelData data = (LevelData)bf.Deserialize(fs);
            fs.Close();

            Name = data.Name;
        }
    }

    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream fs = File.Create(Application.persistentDataPath + "/Level_" + _name + ".dat");

        bf.Serialize(fs, this);
        fs.Close();
    }
}
