using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class ChampionMasteryDto
{
    public long championPointsUntilNextLevel;
    public bool chestGranted;
    public long championId;
    public long lastPlayTime;
    public int championLevel;
    public string summonerId;
    public int championPoints;
    public long championPointsSinceLastLevel;
    public int tokensEarned;
}
