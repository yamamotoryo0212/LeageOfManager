using System.Collections;
using System.Collections.Generic;
using System;

[Serializable]
public class LiveGameMenberData
{
    public string Puuid;
    public string SummonerName;
    public string SummonerID;
    public List<string> MatchIDs;
    public List<MatchDto> MatchDtos = new List<MatchDto>();
    public bool IsMatchDataRequest = false;
    public long ChampionID;
    public bool IsMatchHistory;
    public bool IsWinCount;
    public int WinCount;
    public CurrentGameParticipant CurrentGameParticipant;
    public long SummoneLevel;
    public int TeamID;
    public int Kill;
    public int Death;
    public int Assist;
    public Test LeagueEntryDTO;
    public long FavoriteChampion;
}
