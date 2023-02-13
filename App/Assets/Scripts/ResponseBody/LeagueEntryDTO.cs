using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class LeagueEntryDTO
{
    public string leagueId;
    public string summonerId;
    public string summonerName;
    public string queueType;
    public string tier;
    public string rank;
    public int leaguePoints;
    public int wins;
    public int losses;
    public bool hotStreak;
    public bool veteran;
    public bool freshBlood;
    public bool inactive;
}