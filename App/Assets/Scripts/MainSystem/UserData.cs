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
            Debug.LogError("puuid‚É–³Œø‚È’l‚ª“ü‚è‚Ü‚µ‚½");
            return;
        }
        _puuid = puuid;
    }
    public void SetSummonerId(string summonerID)
    {
        if (summonerID == null && summonerID.Length <= 0)
        {
            Debug.LogError("summonerID‚É–³Œø‚È’l‚ª“ü‚è‚Ü‚µ‚½");
            return;
        }
        _summonerID = summonerID;
    }
}
