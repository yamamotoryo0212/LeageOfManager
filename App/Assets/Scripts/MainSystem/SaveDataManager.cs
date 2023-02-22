using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class SaveDataManager : MonoBehaviour
{
    private string _summonerpath = null;
    public string Summonerpath
    {
        get { return _summonerpath; }
    }

    private string _developPath = null;
    public string DevelopPath
    {
        get { return _developPath; }
    }

    private void Awake()
    {
        _summonerpath = Application.persistentDataPath + "SaveData.json";
        _developPath = Application.persistentDataPath + "DevelopData.json";
    }

    public T Save<T>(T dataType,string path)
    {
        string jsonData = JsonUtility.ToJson(dataType);
        StreamWriter writer = new StreamWriter(path, false);
        writer.WriteLine(jsonData);
        writer.Flush();
        writer.Close();
        return default;
    }

    public T Load<T>(string path)
    {
        try
        {
            StreamReader reader = new StreamReader(path);
            string datastr = reader.ReadToEnd();
            reader.Close();
            return JsonUtility.FromJson<T>(datastr);
        }
        catch
        {
            return default;
        }
    }
}


[Serializable]
public class SaveData
{
    public string SummonerName;
    public string TagLine;
}

[Serializable]
public class DevelopData
{
    public string APIKey;
}