using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SummonerDropdown : MonoBehaviour
{
    [SerializeField]
    private TMP_Dropdown _dropdown = null;
    private bool _isSet = false;


    private void Update()
    {

        if (LOM.Instance.LiveGameManager.IsMatchIDRequest && !_isSet)
        {
            _isSet = true;

            for (int i = 0; i < LOM.Instance.LiveGameManager.LiveGameMenberDatas.Count; i++)
            {
                _dropdown.options.Add(new TMP_Dropdown.OptionData { text = LOM.Instance.LiveGameManager.LiveGameMenberDatas[i].SummonerName });
            }
        }
        if (_dropdown.value <= 0) return;
        SetMatchHistory(_dropdown.value);
    }

    private void SetWinRate(int dropDownValue)
    {

    }

    private void SetMatchHistory(int dropDownValue)
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
