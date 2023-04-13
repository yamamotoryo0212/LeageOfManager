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



        //_isSet = true;
    }

    public void SetWindow()
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

            Image summoner = Instantiate(_summonerData, parent);
            summoner.color = color;
            SummonerData summonerData = summoner.GetComponent<SummonerData>();


            var key = item.SummonerName + "(" + LOM.Instance.RiotIDDataManager.ChampionID[(int)item.ChampionID].ToString() + ")";

            summonerData.SummonerName.text = item.SummonerName;
            summonerData.SummonerLevel.text = LOM.Instance.UIManager.LiveGameUIManager.SetSummonerLevel(key);
            summonerData.ChampionIcon.sprite = LOM.Instance.UIManager.LiveGameUIManager.SetChampionIcon(key);
            summonerData.SummonerSpellIcon[0].sprite = LOM.Instance.UIManager.LiveGameUIManager.SetSummonerSpell(key)[0];
            summonerData.SummonerSpellIcon[1].sprite = LOM.Instance.UIManager.LiveGameUIManager.SetSummonerSpell(key)[1];
            summonerData.RankIcon.sprite = LOM.Instance.UIManager.LiveGameUIManager.SetRankIcon(key);
            summonerData.RankText.text = LOM.Instance.UIManager.LiveGameUIManager.SetRankText(key);
            summonerData.WinRateText.text = LOM.Instance.UIManager.LiveGameUIManager.SetWinRate(key);
            summonerData.RankWinRateText.text = LOM.Instance.UIManager.LiveGameUIManager.SetRankWinRate(key);
            summonerData.KDARateText.text = LOM.Instance.UIManager.LiveGameUIManager.SetKDA(key);

            //LOM.Instance.UIManager.LiveGameUIManager.
        }
        _isSet = true;
    }
}
