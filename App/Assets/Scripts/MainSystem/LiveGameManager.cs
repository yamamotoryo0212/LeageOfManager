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
    private RequestMatchSummonerAPI _summonerDTO_MatchMember = null;

    [Header("�A�J�E���g���N�G�X�g")]
    private string _tagLine = "JP1";
    private string _gameName = "�s���h��";
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
    private int _matchCount = 2;
    public int MatchCount
    {
        get { return _matchCount; }
    }


    [Header("���N�G�X�g�`�F�b�N")]
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
    private bool _isMatchIDRequest = false;
    public bool IsMatchIDRequest
    {
        get { return _isMatchIDRequest; }
        set { _isMatchIDRequest = value; }
    }
    private bool _isMatchSummonerRequest = false;
    public bool IsMatchSummonerRequest
    {
        get { return _isMatchSummonerRequest; }
        set { _isMatchSummonerRequest = value; }
    }

    private List<LiveGameMenberData> _liveGameMenberDatas = new List<LiveGameMenberData>();
    public List<LiveGameMenberData> LiveGameMenberDatas
    {
        get { return _liveGameMenberDatas; }
    }

    private void Awake()
    {
        _requestAccountURL = $"https://asia.api.riotgames.com/riot/account/v1/accounts/by-riot-id/{_gameName}/{_tagLine}?api_key={LOM.Instance.Mainsystem.DevelopmentAPIKey}";
        StartCoroutine(_requestAccountAPI.GetRequest(_requestAccountURL));
    }

    private void Update()
    {
        if (!_isAccountRequest) return;

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

        if (_isSpectatorRequest && !_isMatchSummonerRequest)
        {

        }

        //if (_isSpectatorRequest && !_isMatchIDRequest)
        //{
        //    if (_requestMatchIDURL == null) return;
        //    _isMatchIDRequest = true;
        //    StartCoroutine(_requestMatchIDAPI.GetRequest(_requestMatchIDURL));
        //}
    }

    public void SetMatchMenberPUUID(string pass)
    {
        StartCoroutine(_summonerDTO_MatchMember.GetRequest(pass));
    }
}
