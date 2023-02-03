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

    [Header("アカウントリクエスト")]
    private string _tagLine = "JP1";
    private string _gameName = "つぐちゃん";
    private string _requestAccountURL = null;

    [Header("サモナーリクエスト")]
    private string _requestSummonerURL = null;
    public string RequestSummonerURL
    {
        get { return _requestSummonerURL; }
        set
        {
            if (value == null && value.Length <= 0)
            {
                Debug.LogError("_requestSummonerURLに無効な値が入りました");
                return;
            }
            _requestSummonerURL = value;
        }
    }

    [Header("スペクテータ―リクエスト")]
    private string _requestSpectatorURL = null;
    public string RequestSpectatorURL
    {
        get { return _requestSpectatorURL; }
        set
        {
            if (value == null && value.Length <= 0)
            {
                Debug.LogError("_requestSpectatorURLに無効な値が入りました");
                return;
            }
            _requestSpectatorURL = value;
        }
    }

    [Header("マッチIDリクエスト")]
    private string _requestMatchIDURL = null;
    public string RequestMatchIDURL
    {
        get { return _requestMatchIDURL; }
        set
        {
            if (value == null && value.Length <= 0)
            {
                Debug.LogError("_requestMatchURLに無効な値が入りました");
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

    [Header("マッチリクエスト")]
    private string _requestMatchURL = null;
    public string RequestMatchURL
    {
        get { return _requestMatchURL; }
        set
        {
            if (value == null && value.Length <= 0)
            {
                Debug.LogError("_requestMatchURLに無効な値が入りました");
                return;
            }
            _requestMatchURL = value;
        }
    }
    private int _matchCount = 0;
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

    /// <summary> マッチメンバーのデータは全部こいつに入れる予定 </summary>
    private List<LiveGameMenberData> _liveGameMenberDatas = new List<LiveGameMenberData>();
    public List<LiveGameMenberData> LiveGameMenberDatas
    {
        get { return _liveGameMenberDatas; }
    }

    private float _currentTime = 0f;
    private void Awake()
    {
        _requestAccountURL = $"https://asia.api.riotgames.com/riot/account/v1/accounts/by-riot-id/{_gameName}/{_tagLine}?api_key={LOM.Instance.Mainsystem.DevelopmentAPIKey}";       
    }

    private void Update()
    {
        _currentTime += Time.deltaTime;
        if (_currentTime > 1)
        {
            if (!_isAccountRequest)
            {
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

                _isSpectatorRequest = true;
                StartCoroutine(_requestSpectatorAPI.GetRequest(_requestSpectatorURL));
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

    public void SetMatchMenberData(string pass, long championID)
    {
        StartCoroutine(_requestMatchMemberAPI.GetRequest(pass,championID));
    }

    public void SetMatchIDData(string pass , string puuid)
    {
        StartCoroutine(_requestMatchIDAPI.GetRequest(pass,puuid));
    }

    public void SetMatchData(string pass, string puuid)
    {
        StartCoroutine(_requestMatchAPI.GetRequest(pass, puuid));
    }
}
