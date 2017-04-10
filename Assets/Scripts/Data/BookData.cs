using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[Serializable]
public class BookData
{
    [SerializeField]
    private string _name;

    [SerializeField]
    private int _experience;

    public string Name { get { return _name; } private set { _name = value; } }

    public int Experience { get { return _experience; } private set { _experience = value; } }

    public void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/Book_" + _name + ".dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs = File.Open(Application.persistentDataPath + "/Book_" + _name + ".dat", FileMode.Open);

            BookData data = (BookData)bf.Deserialize(fs);
            fs.Close();

            Name = data.Name;
            Experience = data.Experience;
        }
    }

    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream fs = File.Create(Application.persistentDataPath + "/Book_" + _name + ".dat");

        bf.Serialize(fs, this);
        fs.Close();
    }
}
