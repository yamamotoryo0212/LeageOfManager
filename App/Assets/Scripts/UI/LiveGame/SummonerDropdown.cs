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
        LOM.Instance.UIManager.LiveGameUIManager.SetMatchHistory(_dropdown.value);
        LOM.Instance.UIManager.LiveGameUIManager.WinRate();
    }

    private void SetWinRate(int dropDownValue)
    {

    }
}
