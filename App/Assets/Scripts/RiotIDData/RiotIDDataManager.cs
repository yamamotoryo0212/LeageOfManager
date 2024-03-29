using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiotIDDataManager : MonoBehaviour
{
    private ChampionIDData _championIDData = new ChampionIDData();
    private SummonerSpellIDData _summonerSpellIDData = new SummonerSpellIDData();
    private RankTierData _rankTierData = new RankTierData();

    private Dictionary<int, string> _championID = new Dictionary<int, string>();
    public Dictionary<int, string> ChampionID
    {
        get { return _championID; }
    }

    private Dictionary<int, string> _summonerID = new Dictionary<int, string>();
    public Dictionary<int, string> SummonerID
    {
        get { return _summonerID; }
    }

    private Dictionary<string, string> _rankTierID = new Dictionary<string, string>();
    public Dictionary<string, string> RankTierID
    {
        get { return _rankTierID; }
    }

    private void Awake()
    {
        foreach (string item in _championIDData.ChampionIDs)
        {
            _championID.Add(int.Parse(item.Split(':')[0]), item.Split(':')[1]);
        }

        foreach (var item in _summonerSpellIDData.SummonerSpellID)
        {
            _summonerID.Add(int.Parse(item.Split(':')[0]), item.Split(':')[1]);
        }

        foreach (var item in _rankTierData.Tier)
        {
            _rankTierID.Add(item.Split(':')[0], item.Split(':')[1]);
        }
    }
}
