using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AllSummonerUIManager : MonoBehaviour
{
    [SerializeField]
    private Button _viewChangeButton = null;

    private void Awake()
    {
        _viewChangeButton.onClick.AddListener(() => LOM.Instance.UIManager.LiveGameChangeWindow(UIManager.LiveGameWindowMode.Individual));
    }
}
