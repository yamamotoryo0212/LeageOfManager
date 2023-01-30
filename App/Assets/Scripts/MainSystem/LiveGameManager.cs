using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiveGameManager : MonoBehaviour
{
    [SerializeField]
    private RequestAccountAPI _requestAccountAPI = null;
    [SerializeField]
    private RequestSummonerAPI _requestSummonerAPI = null;

    [Header("アカウントリクエストパラメータ")]
    private string _tagLine = "JP1";
    private string _gameName = "ピリ辛井";
    private string _requestAccountURL = null;

    [Header("サモナーリクエストパラメータ")]
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
    }
}
