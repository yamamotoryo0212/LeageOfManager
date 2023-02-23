using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuWindow : MonoBehaviour
{
    [SerializeField]
    private Button _liveGameButton = null;
    [SerializeField]
    private Button _developButton = null;
    [SerializeField]
    private DebugManager _debugManager = null;

    private void Awake()
    {
        _liveGameButton.onClick.AddListener(() => OpenLiveGameWindow());
        _developButton.onClick.AddListener(() => OpenDebugWindow());
    }

    private void OpenDebugWindow()
    {
        Debug.Log("on");
        _debugManager.gameObject.SetActive(true);
        Debug.Log(_debugManager.gameObject.activeSelf);
        StartCoroutine(LOM.Instance.UIManager.MenuWindowCloseAnim());
    }
    private void OpenLiveGameWindow()
    {
        _debugManager.gameObject.SetActive(false);
        LOM.Instance.LiveGameManager.ResetButton();
        StartCoroutine(LOM.Instance.UIManager.MenuWindowCloseAnim());
    }
}
