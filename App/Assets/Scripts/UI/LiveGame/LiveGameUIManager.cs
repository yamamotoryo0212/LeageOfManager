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
        test.text = _winCount.ToString();
    }

    public void SetMatchHistory(int dropDownValue)
    {
        if (!LOM.Instance.LiveGameManager.LiveGameMenberDatas[dropDownValue].IsMatchHistory)
        {
            LOM.Instance.LiveGameManager.LiveGameMenberDatas[dropDownValue].IsMatchHistory = true;

            for (int i = 0; i < LOM.Instance.LiveGameManager.LiveGameMenberDatas[dropDownValue].MatchIDs.Count; i++)
            {
                if (i == 15)
                {
                    break;
                }
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
                                _winRateText.text = _winCount.ToString();
                            }
                        }
                    }
                }
            }
        }        
    }
}
