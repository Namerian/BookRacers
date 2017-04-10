using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[Serializable]
public class PlayerData
{
    [SerializeField]
    private int _money;

    [SerializeField]
    private int _currentPilotIndex;

    [SerializeField]
    private int _currentBookIndex;

    [SerializeField]
    private int _currentLevelIndex;

    //===============================================================================
    // PROPERTIES
    //===============================================================================

    public int Money
    {
        get { return _money; }
        set { _money = value; }
    }

    public int CurrentPilotIndex
    {
        get { return _currentPilotIndex; }
        set { _currentPilotIndex = value; }
    }

    public int CurrentBookIndex
    {
        get { return _currentBookIndex; }
        set { _currentBookIndex = value; }
    }

    public int CurrentLevelIndex
    {
        get { return _currentLevelIndex; }
        set { _currentLevelIndex = value; }
    }

    //===============================================================================
    // METHODS
    //===============================================================================

    public void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/PlayerData.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs = File.Open(Application.persistentDataPath + "/PlayerData.dat", FileMode.Open);

            PlayerData data = (PlayerData)bf.Deserialize(fs);
            fs.Close();

            Money = data.Money;
            CurrentPilotIndex = CurrentPilotIndex;
            CurrentBookIndex = CurrentBookIndex;
            CurrentLevelIndex = CurrentLevelIndex;
        }
    }

    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream fs = File.Create(Application.persistentDataPath + "/PlayerData.dat");

        bf.Serialize(fs, this);
        fs.Close();
    }
}
