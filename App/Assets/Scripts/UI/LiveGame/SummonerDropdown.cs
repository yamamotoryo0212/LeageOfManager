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
    [SerializeField]
    private TextMeshProUGUI _summonerName = null;
    [SerializeField]
    private TextMeshProUGUI _summonerLevel = null;
    [SerializeField]
    private TextMeshProUGUI _kdaText = null;
    [SerializeField]
    private TextMeshProUGUI _rankText = null;
    [SerializeField]
    private TextMeshProUGUI _rankWinRate = null;
    [SerializeField]
    private Image _backGround = null;
    [SerializeField]
    private Image _summonerSpell001 = null;
    [SerializeField]
    private Image _summonerSpell002 = null;
    [SerializeField]
    private List<Image> _perks = new List<Image>();
    [SerializeField]
    private Image _mainRuneBackGround = null;
    [SerializeField]
    private Image _subRuneBackGround = null;
    [SerializeField]
    private Image _championIcon = null;
    [SerializeField]
    private Image _ranlIcon = null;
    [SerializeField]
    private Image _favoriteChampionIcon = null;

    private bool _isSet = false;
    public bool IsSet
    {
        get { return _isSet; }
        set { _isSet = value; }
    }
    private float _currentTime = 0f;

    private void Update()
    {
        _currentTime += Time.deltaTime;
        if (_currentTime > 1)
        {
          
            _currentTime = 0f;
        }
        if (!LOM.Instance.LiveGameManager.IsMatchIDRequest) return;

        if (LOM.Instance.LiveGameManager.IsMatchIDRequest && !_isSet && LOM.Instance.LiveGameManager.LiveGameMenberDatas.Count == 10)
        {
            _isSet = true;
            _dropdown.options = new List<TMP_Dropdown.OptionData>();

            for (int i = 0; i < LOM.Instance.LiveGameManager.LiveGameMenberDatas.Count; i++)
            {
                _dropdown.options.Add(new TMP_Dropdown.OptionData
                {
                    text = LOM.Instance.LiveGameManager.LiveGameMenberDatas[i].SummonerName +
                              "(" +
                              LOM.Instance.RiotIDDataManager.ChampionID[(int)LOM.Instance.LiveGameManager.LiveGameMenberDatas[i].ChampionID] +
                              ")"
                });
            }
        }

        if (_dropdown.options.Count <= 0) return;

        //TODO:
        if (_dropdown.value == 0)
        {
            foreach (var item in _dropdown.GetComponentsInChildren<TextMeshProUGUI>())
            {
                if (item.name == "Label")
                {
                    item.GetComponent<TextMeshProUGUI>().text = _dropdown.options[0].text;
                }
            }
        }

        LOM.Instance.UIManager.LiveGameUIManager.SetMatchHistory(_dropdown.value);
        _winRateText.text = LOM.Instance.UIManager.LiveGameUIManager.SetWinRate(_dropdown.options[_dropdown.value].text);
        _backGround.sprite = LOM.Instance.UIManager.LiveGameUIManager.SetBackGround(_dropdown.options[_dropdown.value].text);
        _summonerSpell001.sprite = LOM.Instance.UIManager.LiveGameUIManager.SetSummonerSpell(_dropdown.options[_dropdown.value].text)[0];
        _summonerSpell002.sprite = LOM.Instance.UIManager.LiveGameUIManager.SetSummonerSpell(_dropdown.options[_dropdown.value].text)[1];
        _summonerName.text = LOM.Instance.UIManager.LiveGameUIManager.SetSummonerName(_dropdown.options[_dropdown.value].text);
        _summonerLevel.text = LOM.Instance.UIManager.LiveGameUIManager.SetSummonerLevel(_dropdown.options[_dropdown.value].text);
        List<Sprite> sprites = LOM.Instance.UIManager.LiveGameUIManager.SetPerks(_dropdown.options[_dropdown.value].text);
        _kdaText.text = LOM.Instance.UIManager.LiveGameUIManager.SetKDA(_dropdown.options[_dropdown.value].text);
        if (sprites.Count <= 0) return;
        for (int i = 0; i < _perks.Count; i++)
        {
            _perks[i].sprite = sprites[i];
        }
        _mainRuneBackGround.sprite = LOM.Instance.UIManager.LiveGameUIManager.SetMainRuneBackGround(_dropdown.options[_dropdown.value].text);
        _subRuneBackGround.sprite = LOM.Instance.UIManager.LiveGameUIManager.SetSubRuneBackGround(_dropdown.options[_dropdown.value].text);
        _championIcon.sprite = LOM.Instance.UIManager.LiveGameUIManager.SetChampionIcon(_dropdown.options[_dropdown.value].text);
        _favoriteChampionIcon.sprite = LOM.Instance.UIManager.LiveGameUIManager.SetFavoriteChampionIcon(_dropdown.options[_dropdown.value].text);
        _ranlIcon.sprite = LOM.Instance.UIManager.LiveGameUIManager.SetRankIcon(_dropdown.options[_dropdown.value].text);
        _rankText.text = LOM.Instance.UIManager.LiveGameUIManager.SetRankText(_dropdown.options[_dropdown.value].text);
        _rankWinRate.text = LOM.Instance.UIManager.LiveGameUIManager.SetRankWinRate(_dropdown.options[_dropdown.value].text);
        LOM.Instance.LiveGameManager.IsMatchRequest = true;


        for (int i = 0; i < LOM.Instance.LiveGameManager.LiveGameMenberDatas.Count; i++)
        {
            //Debug.Log(LOM.Instance.LiveGameManager.LiveGameMenberDatas[i].SummonerName + " : " + LOM.Instance.LiveGameManager.LiveGameMenberDatas[i].WinCount);
        }
    }
}
