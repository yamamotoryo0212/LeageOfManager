using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LiveGameUIManager : MonoBehaviour
{
    private void Update()
    {

    }

    public void SetMatchHistory(int dropDownValue)
    {
        if (!LOM.Instance.LiveGameManager.LiveGameMenberDatas[dropDownValue].IsMatchHistory)
        {
            for (int i = 0; i < LOM.Instance.LiveGameManager.LiveGameMenberDatas[dropDownValue].MatchIDs.Count; i++)
            {
                if (i == LOM.Instance.LiveGameManager.MatchCount)
                {
                    break;
                }
                LOM.Instance.LiveGameManager.LiveGameMenberDatas[dropDownValue].IsMatchHistory = true;
                string pass = $"https://asia.api.riotgames.com/lol/match/v5/matches/{LOM.Instance.LiveGameManager.LiveGameMenberDatas[dropDownValue].MatchIDs[i]}?api_key={LOM.Instance.Mainsystem.DevelopmentAPIKey}";
                LOM.Instance.LiveGameManager.SetMatchData(pass, LOM.Instance.LiveGameManager.LiveGameMenberDatas[dropDownValue].Puuid);
            }
        }
    }

    public string SetWinRate(string summonerName)
    {
        string str = "Loading....";
        for (int i = 0; i < LOM.Instance.LiveGameManager.LiveGameMenberDatas.Count; i++)
        {
            if (LOM.Instance.LiveGameManager.LiveGameMenberDatas[i].SummonerName +
                "(" +
                LOM.Instance.RiotIDDataManager.ChampionID[(int)LOM.Instance.LiveGameManager.LiveGameMenberDatas[i].ChampionID] +
                ")" == summonerName
               )
            {
                if (LOM.Instance.LiveGameManager.LiveGameMenberDatas[i].MatchDtos.Count == 0) return str;

                string winRate = (((LOM.Instance.LiveGameManager.LiveGameMenberDatas[i].WinCount * 1.0f) / (LOM.Instance.LiveGameManager.LiveGameMenberDatas[i].MatchDtos.Count * 1.0f)) * 100).ToString("N2");
                str = $"����{LOM.Instance.LiveGameManager.MatchCount}��̏��� : " +
                        winRate +
                        "%";
                Debug.Log(LOM.Instance.LiveGameManager.LiveGameMenberDatas[i].SummonerName + " : " + LOM.Instance.LiveGameManager.LiveGameMenberDatas[i].CurrentGameParticipant.teamId);
            }
        }
        return str;
    }

    public Sprite SetBackGround(string summonerName)
    {
        Sprite sprite = Resources.Load<Sprite>("Splash/9999");

        for (int i = 0; i < LOM.Instance.LiveGameManager.LiveGameMenberDatas.Count; i++)
        {
            if (LOM.Instance.LiveGameManager.LiveGameMenberDatas[i].SummonerName + "(" + LOM.Instance.RiotIDDataManager.ChampionID[(int)LOM.Instance.LiveGameManager.LiveGameMenberDatas[i].ChampionID] + ")" == summonerName)
            {
                if (LOM.Instance.LiveGameManager.LiveGameMenberDatas[i].MatchDtos.Count == 0) return sprite;
                Debug.Log($"Splash/{LOM.Instance.LiveGameManager.LiveGameMenberDatas[i].CurrentGameParticipant.championId}");
                sprite = (Sprite)Resources.Load($"Splash/{LOM.Instance.LiveGameManager.LiveGameMenberDatas[i].CurrentGameParticipant.championId}",typeof(Sprite));
            }
        }
        return sprite;
    }
}
