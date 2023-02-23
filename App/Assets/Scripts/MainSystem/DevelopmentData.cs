using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevelopmentData : MonoBehaviour
{
    private string _developmentAPIKey = "";
    public string DevelopmentAPIKey
    {
        get { return _developmentAPIKey; }
    }

    private void Awake()
    {
        LOM.Instance.SaveData.DevelopPath = Application.persistentDataPath + "DevelopData.json";
        try
        {
            _developmentAPIKey = LOM.Instance.SaveData.Load<DevelopData>(LOM.Instance.SaveData.DevelopPath).APIKey;
        }
        catch
        {
            Debug.LogWarning("API Key‚ª“ü‚Á‚Ä‚È‚¢");
        }        
    }

    public void SetAPIKey(string key) 
    {
        if (key ==null || key.Length == 0)
        {
            return;
        }

        try
        {
            _developmentAPIKey = LOM.Instance.SaveData.Load<DevelopData>(LOM.Instance.SaveData.DevelopPath).APIKey;
        }
        catch
        {

        }
        
        _developmentAPIKey = key;
    }
}
