using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiveGameUIManager : MonoBehaviour
{
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
}
