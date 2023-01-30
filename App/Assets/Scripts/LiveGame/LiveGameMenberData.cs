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
    public bool IsRequest = false;
}
