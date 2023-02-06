using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SummonerDropdown : MonoBehaviour
{
    [SerializeField]
    private TMP_Dropdown _dropdown = null;
    [SerializeField]
    private TextMeshProUGUI _winRateText = null;
    private bool _isSet = false;


    private void Update()
    {
        if (LOM.Instance.LiveGameManager.IsMatchIDRequest && !_isSet)
        {
            _isSet = true;

            for (int i = 0; i < LOM.Instance.LiveGameManager.LiveGameMenberDatas.Count; i++)
            {
                _dropdown.options.Add(new TMP_Dropdown.OptionData { text = LOM.Instance.LiveGameManager.LiveGameMenberDatas[i].SummonerName +
                                                                                                                "(" +
                                                                                                                LOM.Instance.RiotIDDataManager.ChampionID[(int)LOM.Instance.LiveGameManager.LiveGameMenberDatas[i].ChampionID] +
                                                                                                                ")"
                                                                                                             });
            }
        }

        if (_dropdown.options.Count <= 0) return;
        LOM.Instance.UIManager.LiveGameUIManager.SetMatchHistory(_dropdown.value);
        _winRateText.text = LOM.Instance.UIManager.LiveGameUIManager.SetWinRate(_dropdown.options[_dropdown.value].text);

        for (int i = 0; i < LOM.Instance.LiveGameManager.LiveGameMenberDatas.Count; i++)
        {
            Debug.Log(LOM.Instance.LiveGameManager.LiveGameMenberDatas[i].SummonerName + " : " + LOM.Instance.LiveGameManager.LiveGameMenberDatas[i].WinCount);
        }
    }
}
