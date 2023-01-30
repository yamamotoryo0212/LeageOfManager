using System;
using System.Collections.Generic;

[Serializable]
public class CurrentGameInfo
{
    public long gameId;
    public string gameType;
    public long gameStartTime;
    public long mapId;
    public long gameLength;
    public string platformId;
    public string gameMode;
    public List<BannedChampion> bannedChampions;
    public long gameQueueConfigId;
    public Observer observers;
    public List<CurrentGameParticipant> participants;
}

[Serializable]
public class BannedChampion
{
    public int pickTurn;
    public long championId;
    public long teamId;
}

[Serializable]
public class Observer
{
    public string encryptionKey;
}

[Serializable]
public class CurrentGameParticipant
{
    public long championId;
    public Perks perks;
    public long profileIconId;
    public bool bot;
    public long teamId;
    public string summonerName;
    public string summonerId;
    public long spell1Id;
    public long spell2Id;
    public List<GameCustomizationObject> gameCustomizationObjects;
}

[Serializable]
public class Perks
{
    public List<long> perkIds;
    public long perkStyle;
    public long perkSubStyle;
}

[Serializable]
public class GameCustomizationObject
{
    public string category;
    public string content;
}