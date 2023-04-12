using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AllSummonerUIManager : MonoBehaviour
{
    [SerializeField]
    private Button _viewChangeButton = null;
    [SerializeField]
    private GameObject _redTeam = null;
    [SerializeField]
    private GameObject _blueTeam = null;

    private Image _summonerData = null;

    private bool _isSet = false;

    private void Awake()
    {
        _viewChangeButton.onClick.AddListener(() => LOM.Instance.UIManager.LiveGameChangeWindow(UIManager.LiveGameWindowMode.Individual));
        _summonerData = (Image)Resources.Load("Prefabs/SummonerData", typeof(Image));
    }

    private void Update()
    {
        if (!LOM.Instance.LiveGameManager.IsMatchIDRequest) return;
        if (!(LOM.Instance.LiveGameManager.LiveGameMenberDatas.Count == 10)) return;
        if (_isSet) return;

        foreach (LiveGameMenberData item in LOM.Instance.LiveGameManager.LiveGameMenberDatas)
        {
            Transform parent = null;
            Color color = new Color();
            if (item.TeamID == 100)
            {
                parent = _blueTeam.transform;
                color = new Color(0.419f, 0.419f, 1f, 0.6f); ;
            }
            else if (item.TeamID == 200)
            {
                parent = _redTeam.transform;
                color = new Color(0.737f, 0.196f, 0.196f, 0.6f);
            }

            Image summoner =  Instantiate(_summonerData, parent);
            summoner.color = color;
        }
        _isSet = true;
    }
}
