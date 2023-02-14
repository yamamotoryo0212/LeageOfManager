using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class RankTierData
{
    public List<string> Tier = new List<string>()
    {
        "IRON:アイアン",
        "BRONZE:ブロンズ",
        "SILVER:シルバー",
        "GOLD:ゴールド",
        "PLATINUM:プラチナ",
        "DIAMOND:ダイヤモンド",
        "MASTER:マスター",
        "GRANDMASTER:グランドマスター",
        "CHALLENGER:チャレンジャー"
    };
}