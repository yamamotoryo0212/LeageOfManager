using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserData : MonoBehaviour
{
    private string _puuid = null;
    public string Puuid
    {
        get { return _puuid; }
    }
    private string _summonerID = null;
    public string SummonerID
    {
        get { return _summonerID; }
    }

    public void SetPuuid(string puuid)
    {
        if (puuid == null && puuid.Length <= 0)
        {
            Debug.LogError("puuid�ɖ����Ȓl������܂���");
            return;
        }
        _puuid = puuid;
    }
    public void SetSummonerId(string summonerID)
    {
        if (summonerID == null && summonerID.Length <= 0)
        {
            Debug.LogError("summonerID�ɖ����Ȓl������܂���");
            return;
        }
        _summonerID = summonerID;
    }
}
