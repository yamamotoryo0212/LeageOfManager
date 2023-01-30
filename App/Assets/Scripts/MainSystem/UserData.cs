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

    public void SetPuuid(string puuid)
    {
        if (puuid == null && puuid.Length <= 0)
        {
            Debug.LogError("puuidに無効な値が入りました");
            return;
        }
        _puuid = puuid;
    }
}
