using System.Collections;
using System.Collections.Generic;
using System;

[Serializable]
public class MatchDto
{
    public MetadataDto metadata;
    public InfoDto info;
}

[Serializable]
public class MetadataDto
{
    public string dataVersion;
    public string matchId;
    public List<string> participants;
}

[Serializable]
public class InfoDto
{
    public long gameCreation;
    public long gameDuration;
    public long gameEndTimestamp;
    public long gameId;
    public string gameMode;
    public string gameName;
    public long gameStartTimestamp;
    public string gameType;
    public string gameVersion;
    public int mapId;
    public List<ParticipantDto> participants;
    public string platformId;
    public int queueId;
    public List<TeamDto> teams;
    public string tournamentCode;
}

[Serializable]
public class ParticipantDto
{
    public int assists;
    public int baronKills;
    public int bountyLevel;
    public int champExperience;
    public int champLevel;
    public int championId;
    public string championName;
    public int championTransform;
    public int consumablesPurchased;
    public int damageDealtToBuildings;
    public int damageDealtToObjectives;
    public int damageDealtToTurrets;
    public int damageSelfMitigated;
    public int deaths;
    public int detectorWardsPlaced;
    public int doubleKills;
    public int dragonKills;
    public bool firstBloodAssist;
    public bool firstBloodKill;
    public bool firstTowerAssist;
    public bool firstTowerKill;
    public bool gameEndedInEarlySurrender;
    public bool gameEndedInSurrender;
    public int goldEarned;
    public int goldSpent;
    public string individualPosition;
    public int inhibitorKills;
    public int inhibitorTakedowns;
    public int inhibitorsLost;
    public int item0;
    public int item1;
    public int item2;
    public int item3;
    public int item4;
    public int item5;
    public int item6;
    public int itemsPurchased;
    public int killingSprees;
    public int kills;
    public string lane;
    public int largestCriticalStrike;
    public int largestKillingSpree;
    public int largestMultiKill;
    public int longestTimeSpentLiving;
    public int magicDamageDealt;
    public int magicDamageDealtToChampions;
    public int magicDamageTaken;
    public int neutralMinionsKilled;
    public int nexusKills;
    public int nexusTakedowns;
    public int nexusLost;
    public int objectivesStolen;
    public int objectivesStolenAssists;
    public int participantId;
    public int pentaKills;
    public PerksDto perks;
    public int physicalDamageDealt;
    public int physicalDamageDealtToChampions;
    public int physicalDamageTaken;
    public int profileIcon;
    public string puuid;
    public int quadraKills;
    public string riotIdName;
    public string riotIdTagline;
    public string role;
    public int sightWardsBoughtInGame;
    public int spell1Casts;
    public int spell2Casts;
    public int spell3Casts;
    public int spell4Casts;
    public int summoner1Casts;
    public int summoner1Id;
    public int summoner2Casts;
    public int summoner2Id;
    public string summonerId;
    public int summonerLevel;
    public string summonerName;
    public bool teamEarlySurrendered;
    public int teamId;
    public string teamPosition;
    public int timeCCingOthers;
    public int timePlayed;
    public int totalDamageDealt;
    public int totalDamageDealtToChampions;
    public int totalDamageShieldedOnTeammates;
    public int totalDamageTaken;
    public int totalHeal;
    public int totalHealsOnTeammates;
    public int totalMinionsKilled;
    public int totalTimeCCDealt;
    public int totalTimeSpentDead;
    public int totalUnitsHealed;
    public int tripleKills;
    public int trueDamageDealt;
    public int trueDamageDealtToChampions;
    public int trueDamageTaken;
    public int turretKills;
    public int turretTakedowns;
    public int turretsLost;
    public int unrealKills;
    public int visionScore;
    public int visionWardsBoughtInGame;
    public int wardsKilled;
    public int wardsPlaced;
    public bool win;
}


[Serializable]
public class PerksDto
{
    public PerkStatsDto statPerks;
    public List<PerkStyleDto> styles;
}

[Serializable]
public class PerkStatsDto
{
    public int defense;
    public int flex;
    public int offense;
}

[Serializable]
public class PerkStyleDto
{
    public string description;
    public List<PerkStyleSelectionDto> selections;
    public int style;
}

[Serializable]
public class PerkStyleSelectionDto
{
    public int perk;
    public int var1;
    public int var2;
    public int var3;
}

[Serializable]
public class TeamDto
{
    public List<BanDto> bans;
    public ObjectivesDto objectives;
    public int teamId;
    public bool win;
}

[Serializable]
public class BanDto
{
    public int championId;
    public int pickTurn;
}

[Serializable]
public class ObjectivesDto
{
    public ObjectiveDto baron;
    public ObjectiveDto champion;
    public ObjectiveDto dragon;
    public ObjectiveDto inhibitor;
    public ObjectiveDto riftHerald;
    public ObjectiveDto tower;
}

[Serializable]
public class ObjectiveDto
{
    public bool first;
    public int kills;
}