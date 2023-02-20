using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class SaveDataManager : MonoBehaviour
{
    private string _datapath = null;
    private void Awake()
    {
        _datapath = Application.persistentDataPath + "SaveData.json";
        Debug.Log(Application.persistentDataPath + "SaveData.json");
    }

    public void Save(SaveData saveData)
    {
        string jsonData = JsonUtility.ToJson(saveData);
        StreamWriter writer = new StreamWriter(_datapath, false);
        writer.WriteLine(jsonData);
        writer.Flush();
        writer.Close();
    }

    public SaveData Load()
    {
        try
        {
            StreamReader reader = new StreamReader(_datapath);
            string datastr = reader.ReadToEnd();
            reader.Close();
            return JsonUtility.FromJson<SaveData>(datastr);
        }
        catch
        {
            return null;
        }
    }
}


[Serializable]
public class SaveData
{
    public string SummonerName;
    public string TagLine;
}