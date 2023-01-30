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

    [Header("�A�J�E���g���N�G�X�g�p�����[�^")]
    private string _tagLine = "JP1";
    private string _gameName = "toplane";
    private string _requestAccountURL = null;

    [Header("�T���i�[���N�G�X�g�p�����[�^")]
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
    }
}
