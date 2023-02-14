using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Test
{
    public List<LeagueEntryDTO> leagueEntryDTOs = new List<LeagueEntryDTO>();
}

[Serializable]
public class LeagueEntryDTO
{
    public string leagueId;
    public string queueType;
    public string tier;
    public string rank;
    public string summonerId;
    public string summonerName;
    public int leaguePoints;
    public int wins;
    public int losses;
    public bool veteran;
    public bool inactive;
    public bool freshBlood;
    public bool hotStreak;
}