using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DebugManager : MonoBehaviour
{
    [SerializeField]
    private ScrollRect _debugConsoll = null;
    [SerializeField]
    private Text _debugText = null;
    [SerializeField]
    private TMP_InputField _developmentAPIKeyField = null;
    [SerializeField]
    private Button _registerButton = null;

    private DevelopData _developData = null;

    private void Awake()
    {
        _developData = new DevelopData();
        Application.logMessageReceived += LoggedCb;
        _registerButton.onClick.AddListener(() => RegisterAPIKey());
    }

    private void LoggedCb(string logstr, string stacktrace, LogType type)
    {
        _debugText.GetComponent<Text>().text += logstr;
        _debugText.GetComponent<Text>().text += "\n";
        _debugConsoll.GetComponent<ScrollRect>().verticalNormalizedPosition = 0;
        _debugText.GetComponent<Text>().text += "\n";
    }

    private void RegisterAPIKey()
    {
        LOM.Instance.Mainsystem.SetAPIKey(_developmentAPIKeyField.text);
        _developData.APIKey = _developmentAPIKeyField.text;
        LOM.Instance.SaveData.Save(_developData, LOM.Instance.SaveData.DevelopPath);
    }
}
