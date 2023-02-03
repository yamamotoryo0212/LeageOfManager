using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LiveGameUIManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _winRateText = null;
    [SerializeField]
    private TextMeshProUGUI test = null;
    private int _winCount = 0;
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

    public void WinRate()
    {
        _winCount = 0;
        _winRateText.text = "Not Found";
        for (int i = 0; i < LOM.Instance.LiveGameManager.LiveGameMenberDatas.Count; i++)
        {
            if (LOM.Instance.LiveGameManager.LiveGameMenberDatas[i].IsMatchDataRequest)
            {
                for (int j = 0; j < LOM.Instance.LiveGameManager.LiveGameMenberDatas[i].MatchDtos.Count; j++)
                {
                    for (int k = 0; k < LOM.Instance.LiveGameManager.LiveGameMenberDatas[i].MatchDtos[j].info.participants.Count; k++)
                    {
                        if (LOM.Instance.LiveGameManager.LiveGameMenberDatas[i].MatchDtos[j].info.participants[k].summonerId ==
                            LOM.Instance.LiveGameManager.LiveGameMenberDatas[i].SummonerID)
                        {
                            if (LOM.Instance.LiveGameManager.LiveGameMenberDatas[i].MatchDtos[j].info.participants[k].win)
                            {
                                _winCount++;
                                LOM.Instance.LiveGameManager.LiveGameMenberDatas[i].WinCount++;
                                Debug.Log((LOM.Instance.LiveGameManager.LiveGameMenberDatas[i].MatchDtos[j].info.participants[k].summonerName +
                                                   LOM.Instance.LiveGameManager.LiveGameMenberDatas[i].WinCount));
                            }
                        }
                    }
                }
            }
        }        
    }

    public void Test(int dropDownValue)
    {
        int summonerNum = -1;
        int participants = -1;
        bool iswin = false;
        int winCount = 0;

        for (int i = 0; i < LOM.Instance.LiveGameManager.LiveGameMenberDatas.Count; i++)
        {
            if (LOM.Instance.LiveGameManager.LiveGameMenberDatas[i].SummonerID == 
                LOM.Instance.LiveGameManager.LiveGameMenberDatas[dropDownValue].SummonerID)
            {
                summonerNum = i;
            }
        }

        if (summonerNum >= 0)
        {
            for (int i = 0; i < LOM.Instance.LiveGameManager.LiveGameMenberDatas[summonerNum].MatchDtos.Count; i++)
            {
                for (int j = 0; j < LOM.Instance.LiveGameManager.LiveGameMenberDatas[summonerNum].MatchDtos[i].info.participants.Count; j++)
                {
                    if (LOM.Instance.LiveGameManager.LiveGameMenberDatas[summonerNum].MatchDtos[i].info.participants[j].summonerId ==
                    LOM.Instance.LiveGameManager.LiveGameMenberDatas[dropDownValue].SummonerID)
                    {
                        participants = j;
                        iswin = true;
                    }
                }
            }
        }

        if (iswin)
        {
            for (int i = 0; i < LOM.Instance.LiveGameManager.LiveGameMenberDatas[summonerNum].MatchDtos.Count; i++)
            {
                if (LOM.Instance.LiveGameManager.LiveGameMenberDatas[summonerNum].MatchDtos[i].info.participants[participants].win)
                {
                    winCount++;
                    LOM.Instance.LiveGameManager.LiveGameMenberDatas[summonerNum].WinCount = winCount;
                    _winRateText.text = LOM.Instance.LiveGameManager.LiveGameMenberDatas[summonerNum].WinCount.ToString();
                }
            }            
        }
    }
}
