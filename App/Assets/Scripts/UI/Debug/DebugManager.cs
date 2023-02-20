using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DebugManager : MonoBehaviour
{
    [SerializeField]
    private Toggle _debugToggle = null;
    [SerializeField]
    private ScrollRect _debugConsoll = null;
    [SerializeField]
    private Text _debugText = null;

    private void Awake()
    {
        Application.logMessageReceived += LoggedCb;

        _debugConsoll.gameObject.SetActive(false);
        _debugToggle.onValueChanged.AddListener((value) => DebugWindow(value));
    }

    private void DebugWindow(bool value)
    {
        _debugConsoll.gameObject.SetActive(value);
    }
    public void LoggedCb(string logstr, string stacktrace, LogType type)
    {
        _debugText.GetComponent<Text>().text += logstr;
        _debugText.GetComponent<Text>().text += "\n";
        // 常にTextの最下部（最新）を表示するように強制スクロール
        _debugConsoll.GetComponent<ScrollRect>().verticalNormalizedPosition = 0;
        _debugText.GetComponent<Text>().text += "\n";
    }
}
