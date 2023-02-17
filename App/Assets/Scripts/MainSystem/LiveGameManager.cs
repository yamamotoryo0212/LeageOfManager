using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiveGameManager : MonoBehaviour
{
    [Header("Requests")]
    [SerializeField]
    private RequestAccountAPI _requestAccountAPI = null;
    [SerializeField]
    private RequestSummonerAPI _requestSummonerAPI = null;
    [SerializeField]
    private RequestSpectatorAPI _requestSpectatorAPI = null;
    [SerializeField]
    private RequestMatchIDAPI _requestMatchIDAPI = null;
    [SerializeField]
    private RequestMatchSummonerAPI _requestMatchMemberAPI = null;
    [SerializeField]
    private RequestMatchAPI _requestMatchAPI = null;
    [SerializeField]
    private RequestLeageAPI _requestLeageAPI = null;

    [Header("�A�J�E���g���N�G�X�g")]
    private string _tagLine = "JP1";
    private string _gameName = "";
    private string _requestAccountURL = null;

    [Header("�T���i�[���N�G�X�g")]
    private string _requestSummonerURL = null;
    public string RequestSummonerURL
    {
        get { return _requestSummonerURL; }
        set
        {
            if (value == null && value.Length <= 0)
            {
                Debug.LogError("_requestSummonerURL�ɖ����Ȓl������܂���");
                return;
            }
            _requestSummonerURL = value;
        }
    }

    [Header("�X�y�N�e�[�^�\���N�G�X�g")]
    private string _requestSpectatorURL = null;
    public string RequestSpectatorURL
    {
        get { return _requestSpectatorURL; }
        set
        {
            if (value == null && value.Length <= 0)
            {
                Debug.LogError("_requestSpectatorURL�ɖ����Ȓl������܂���");
                return;
            }
            _requestSpectatorURL = value;
        }
    }

    [Header("�}�b�`ID���N�G�X�g")]
    private string _requestMatchIDURL = null;
    public string RequestMatchIDURL
    {
        get { return _requestMatchIDURL; }
        set
        {
            if (value == null && value.Length <= 0)
            {
                Debug.LogError("_requestMatchURL�ɖ����Ȓl������܂���");
                return;
            }
            _requestMatchIDURL = value;
        }
    }
    private int _matchIDCount = 100;
    public int MatchIDCount
    {
        get { return _matchIDCount; }
    }

    [Header("�}�b�`���N�G�X�g")]
    private string _requestMatchURL = null;
    public string RequestMatchURL
    {
        get { return _requestMatchURL; }
        set
        {
            if (value == null && value.Length <= 0)
            {
                Debug.LogError("_requestMatchURL�ɖ����Ȓl������܂���");
                return;
            }
            _requestMatchURL = value;
        }
    }
    private int _matchCount = 10;


    public void ResetButton( GameObject gameObject)
    {
        gameObject.SetActive(true);
        LOM.Instance.UIManager.LiveGameUIManager.SummonerDropdown.IsSet = false;
        LOM.Instance.UIManager.LiveGameUIManager.LoadingWindow.gameObject.SetActive(true);
        LOM.Instance.UIManager.LiveGameUIManager.IsLoad = false;
        LOM.Instance.UIManager.LiveGameUIManager.IsSearchWindow = false;
        _liveGameMenberDatas = new List<LiveGameMenberData>();
        _isSearch = false;
        _isAccountRequest = false;
        _isMatchIDRequest = false;
        _isMatchRequest = false;
        _isMatchSummoner = false;
        _isSpectatorRequest = false;
        _isSummonerRequest = false;
        _requestSpectatorURL = null;
    }

    private bool _isSearch = false;
    public bool IsSearch
    {
        get { return _isSearch; }
        set { _isSearch = value; }
    }
    public int MatchCount
    {
        get { return _matchCount; }
    }
    private bool _isAccountRequest = false;
    public bool IsAccountRequest
    {
        get { return _isAccountRequest; }
        set { _isAccountRequest = value; }
    }
    private bool _isSummonerRequest = false;
    public bool IsSummonerRequest
    {
        get { return _isSummonerRequest; }
        set { _isSummonerRequest = value; }
    }
    private bool _isSpectatorRequest = false;
    public bool IsSpectatorRequest
    {
        get { return _isSpectatorRequest; }
        set { _isSpectatorRequest = value; }
    }
    private bool _isMatchSummoner = false;
    public bool IsMatchSummonerRequest
    {
        get { return _isMatchSummoner; }
        set { _isMatchSummoner = value; }
    }
    private bool _isMatchIDRequest = false;
    public bool IsMatchIDRequest
    {
        get { return _isMatchIDRequest; }
        set { _isMatchIDRequest = value; }
    }
    private bool _isMatchRequest = false;
    public bool IsMatchRequest
    {
        get { return _isMatchRequest; }
        set { _isMatchRequest = value; }
    }

    /// <summary> �}�b�`�����o�[�̃f�[�^�͑S�������ɓ����\�� </summary>
    private List<LiveGameMenberData> _liveGameMenberDatas = new List<LiveGameMenberData>();
    public List<LiveGameMenberData> LiveGameMenberDatas
    {
        get { return _liveGameMenberDatas; }
    }

    private float _currentTime = 0f;

    private void Update()
    {
        _currentTime += Time.deltaTime;
        if (_currentTime > 1)
        {
            if (!_isSearch)
            {
                return;
            }

            if (!_isAccountRequest)
            {
                _requestAccountURL = $"https://asia.api.riotgames.com/riot/account/v1/accounts/by-riot-id/{_gameName}/{_tagLine}?api_key={LOM.Instance.Mainsystem.DevelopmentAPIKey}";
                StartCoroutine(_requestAccountAPI.GetRequest(_requestAccountURL));
            }

            if (_isAccountRequest && !_isSummonerRequest)
            {
                _isSummonerRequest = true;
                StartCoroutine(_requestSummonerAPI.GetRequest(_requestSummonerURL));
            }

            if (_isSummonerRequest && !_isSpectatorRequest)
            {
                if (_requestSpectatorURL == null) return;

                
                StartCoroutine(_requestSpectatorAPI.GetRequest(_requestSpectatorURL));
            }

            if (_liveGameMenberDatas.Count == 10)
            {
                _isSpectatorRequest = true;
            }

            //if (LiveGameMenberDatas.Count >= 0)
            //{
            //    for (int i = 0; i < _liveGameMenberDatas.Count; i++)
            //    {
            //        if (_liveGameMenberDatas[i].MatchIDs == null) return;
            //        for (int j = 0; j < _liveGameMenberDatas[i].MatchIDs.Count; j++)
            //        {
            //            if (_liveGameMenberDatas[i].IsRequest) return;
            //            _requestMatchURL = $"https://asia.api.riotgames.com/lol/match/v5/matches/{_liveGameMenberDatas[i].MatchIDs[j]}?api_key={LOM.Instance.Mainsystem.DevelopmentAPIKey}";
            //            StartCoroutine(_requestMatchAPI.GetRequest(_requestMatchURL, _liveGameMenberDatas[i].Puuid));
            //        }
            //    }
            //}
            _currentTime = 0f;
        }                   
    }

    public void SetMatchMenberData(string pass, CurrentGameParticipant currentGameParticipant)
    {
        StartCoroutine(_requestMatchMemberAPI.GetRequest(pass,currentGameParticipant));
    }

    public void SetMatchIDData(string pass , string puuid)
    {
        StartCoroutine(_requestMatchIDAPI.GetRequest(pass,puuid));
    }

    public void SetLeageData(string pass, string puuid)
    {
        StartCoroutine(_requestLeageAPI.GetRequest(pass, puuid));
    }

    public void SetMatchData(string pass, string puuid)
    {
        StartCoroutine(_requestMatchAPI.GetRequest(pass, puuid));
    }

    public void SetSummonerName(string gameName)
    {
        _gameName = gameName;
    }
    public void SetTagLine(string tagLine)
    {
        _tagLine = tagLine;
    }

}
