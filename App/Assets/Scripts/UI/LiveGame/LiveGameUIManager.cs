using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LiveGameUIManager : MonoBehaviour
{
    private void Update()
    {

    }

    public void SetMatchHistory(int dropDownValue)
    {
        if (!LOM.Instance.LiveGameManager.LiveGameMenberDatas[dropDownValue].IsMatchHistory)
        {
            for (int i = 0; i < LOM.Instance.LiveGameManager.LiveGameMenberDatas[dropDownValue].MatchIDs.Count; i++)
            {
                if (i == LOM.Instance.LiveGameManager.MatchCount)
                {
                    break;
                }
                LOM.Instance.LiveGameManager.LiveGameMenberDatas[dropDownValue].IsMatchHistory = true;
                string pass = $"https://asia.api.riotgames.com/lol/match/v5/matches/{LOM.Instance.LiveGameManager.LiveGameMenberDatas[dropDownValue].MatchIDs[i]}?api_key={LOM.Instance.Mainsystem.DevelopmentAPIKey}";
                LOM.Instance.LiveGameManager.SetMatchData(pass, LOM.Instance.LiveGameManager.LiveGameMenberDatas[dropDownValue].Puuid);
            }
        }
    }

    public string SetWinRate(string summonerName)
    {
        string str = "Loading....";
        for (int i = 0; i < LOM.Instance.LiveGameManager.LiveGameMenberDatas.Count; i++)
        {
            if (LOM.Instance.LiveGameManager.LiveGameMenberDatas[i].SummonerName +
                "(" +
                LOM.Instance.RiotIDDataManager.ChampionID[(int)LOM.Instance.LiveGameManager.LiveGameMenberDatas[i].ChampionID] +
                ")" == summonerName
               )
            {
                if (LOM.Instance.LiveGameManager.LiveGameMenberDatas[i].MatchDtos.Count == 0) return str;

                string winRate = (((LOM.Instance.LiveGameManager.LiveGameMenberDatas[i].WinCount * 1.0f) / (LOM.Instance.LiveGameManager.LiveGameMenberDatas[i].MatchDtos.Count * 1.0f)) * 100).ToString("N2");
                str = $"’¼‹ß{LOM.Instance.LiveGameManager.MatchCount}í‚ÌŸ—¦ : " +
                        winRate +
                        "%";
                Debug.Log(LOM.Instance.LiveGameManager.LiveGameMenberDatas[i].SummonerName + " : " + LOM.Instance.LiveGameManager.LiveGameMenberDatas[i].CurrentGameParticipant.teamId);
                //Debug.Log(LOM.Instance.LiveGameManager.LiveGameMenberDatas[i].SummonerName + " : " + LOM.Instance.LiveGameManager.LiveGameMenberDatas[i].CurrentGameParticipant.spell1Id);
            }
        }
        return str;
    }

    public string SetKDA(string summonerName)
    {
        float k = 0.0f;
        float d = 0.0f;
        float a = 0.0f;
        string str = $"’¼‹ß{LOM.Instance.LiveGameManager.MatchCount}í‚ÌKDA : ?/?/?";

        for (int i = 0; i < LOM.Instance.LiveGameManager.LiveGameMenberDatas.Count; i++)
        {
            if (LOM.Instance.LiveGameManager.LiveGameMenberDatas[i].SummonerName +
             "(" +
             LOM.Instance.RiotIDDataManager.ChampionID[(int)LOM.Instance.LiveGameManager.LiveGameMenberDatas[i].ChampionID] +
             ")" == summonerName
            )
            {
                if (LOM.Instance.LiveGameManager.LiveGameMenberDatas[i].MatchDtos.Count == 0) return str;

                k = (float)LOM.Instance.LiveGameManager.LiveGameMenberDatas[i].Kill / (float)LOM.Instance.LiveGameManager.MatchCount;
                d = (float)LOM.Instance.LiveGameManager.LiveGameMenberDatas[i].Death / (float)LOM.Instance.LiveGameManager.MatchCount;
                a = (float)LOM.Instance.LiveGameManager.LiveGameMenberDatas[i].Assist / (float)LOM.Instance.LiveGameManager.MatchCount;
                str = $"’¼‹ß{LOM.Instance.LiveGameManager.MatchCount}í‚ÌKDA : " +
                        $"{ k}" + "/" +
                        $"{ d}" + "/" +
                        $"{ a}" ;
            }
        }
        return str;
    }

    public Sprite SetBackGround(string summonerName)
    {
        Sprite sprite = Resources.Load<Sprite>("Splash/9999");

        for (int i = 0; i < LOM.Instance.LiveGameManager.LiveGameMenberDatas.Count; i++)
        {
            if (LOM.Instance.LiveGameManager.LiveGameMenberDatas[i].SummonerName + "(" + LOM.Instance.RiotIDDataManager.ChampionID[(int)LOM.Instance.LiveGameManager.LiveGameMenberDatas[i].ChampionID] + ")" == summonerName)
            {
                if (LOM.Instance.LiveGameManager.LiveGameMenberDatas[i].MatchDtos.Count == 0) return sprite;
                //Debug.Log($"Splash/{LOM.Instance.LiveGameManager.LiveGameMenberDatas[i].CurrentGameParticipant.championId}");
                sprite = (Sprite)Resources.Load($"Splash/{LOM.Instance.LiveGameManager.LiveGameMenberDatas[i].CurrentGameParticipant.championId}",typeof(Sprite));
            }
        }
        return sprite;
    }

    public Sprite[] SetSummonerSpell(string summonerName)
    {
        Sprite[] sprite = new Sprite[] { Resources.Load<Sprite>("SummoneSpellIcon/999"), Resources.Load<Sprite>("SummoneSpellIcon/999") };

        for (int i = 0; i < LOM.Instance.LiveGameManager.LiveGameMenberDatas.Count; i++)
        {
            if (LOM.Instance.LiveGameManager.LiveGameMenberDatas[i].SummonerName + "(" + LOM.Instance.RiotIDDataManager.ChampionID[(int)LOM.Instance.LiveGameManager.LiveGameMenberDatas[i].ChampionID] + ")" == summonerName)
            {
                if (LOM.Instance.LiveGameManager.LiveGameMenberDatas[i].MatchDtos.Count == 0) return sprite;
                //Debug.Log($"Splash/{LOM.Instance.LiveGameManager.LiveGameMenberDatas[i].CurrentGameParticipant.championId}");
                sprite[0] = (Sprite)Resources.Load($"SummoneSpellIcon/{LOM.Instance.LiveGameManager.LiveGameMenberDatas[i].CurrentGameParticipant.spell1Id}", typeof(Sprite));
                sprite[1] = (Sprite)Resources.Load($"SummoneSpellIcon/{LOM.Instance.LiveGameManager.LiveGameMenberDatas[i].CurrentGameParticipant.spell2Id}", typeof(Sprite));
            }
        }
        return sprite;
    }

    public string SetSummonerName(string summonerName)
    {
        string name = "Loading...";
        for (int i = 0; i < LOM.Instance.LiveGameManager.LiveGameMenberDatas.Count; i++)
        {
            if (LOM.Instance.LiveGameManager.LiveGameMenberDatas[i].SummonerName + "(" + LOM.Instance.RiotIDDataManager.ChampionID[(int)LOM.Instance.LiveGameManager.LiveGameMenberDatas[i].ChampionID] + ")" == summonerName)
            {
                if (LOM.Instance.LiveGameManager.LiveGameMenberDatas[i].MatchDtos.Count == 0) return name;
                name = LOM.Instance.LiveGameManager.LiveGameMenberDatas[i].SummonerName;
            }
        }
        return name;
    }

    public string SetSummonerLevel(string summonerName)
    {
        string name = "Loading...";
        for (int i = 0; i < LOM.Instance.LiveGameManager.LiveGameMenberDatas.Count; i++)
        {
            if (LOM.Instance.LiveGameManager.LiveGameMenberDatas[i].SummonerName + "(" + LOM.Instance.RiotIDDataManager.ChampionID[(int)LOM.Instance.LiveGameManager.LiveGameMenberDatas[i].ChampionID] + ")" == summonerName)
            {
                if (LOM.Instance.LiveGameManager.LiveGameMenberDatas[i].MatchDtos.Count == 0) return name;
                name =  "Lv : "+ LOM.Instance.LiveGameManager.LiveGameMenberDatas[i].SummoneLevel.ToString();
            }
        }
        return name;
    }

    public List<Sprite> SetPerks(string summonerName)
    {
        List<Sprite> sprite = new List<Sprite>();

        for (int i = 0; i < LOM.Instance.LiveGameManager.LiveGameMenberDatas.Count; i++)
        {
            if (LOM.Instance.LiveGameManager.LiveGameMenberDatas[i].SummonerName + "(" + LOM.Instance.RiotIDDataManager.ChampionID[(int)LOM.Instance.LiveGameManager.LiveGameMenberDatas[i].ChampionID] + ")" == summonerName)
            {
                if (LOM.Instance.LiveGameManager.LiveGameMenberDatas[i].MatchDtos.Count == 0) return sprite;

                for (int j = 0; j < LOM.Instance.LiveGameManager.LiveGameMenberDatas[i].CurrentGameParticipant.perks.perkIds.Count; j++)
                {
                    sprite.Add((Sprite)Resources.Load($"Perks/{LOM.Instance.LiveGameManager.LiveGameMenberDatas[i].CurrentGameParticipant.perks.perkIds[j]}", typeof(Sprite)));
                }             
            }
        }
        return sprite;
    }
    public Sprite SetMainRuneBackGround(string summonerName)
    {
        Sprite sprite = Resources.Load<Sprite>("Perks/9999");

        for (int i = 0; i < LOM.Instance.LiveGameManager.LiveGameMenberDatas.Count; i++)
        {
            if (LOM.Instance.LiveGameManager.LiveGameMenberDatas[i].SummonerName + "(" + LOM.Instance.RiotIDDataManager.ChampionID[(int)LOM.Instance.LiveGameManager.LiveGameMenberDatas[i].ChampionID] + ")" == summonerName)
            {
                if (LOM.Instance.LiveGameManager.LiveGameMenberDatas[i].MatchDtos.Count == 0) return sprite;
                
                sprite = (Sprite)Resources.Load($"Perks/{LOM.Instance.LiveGameManager.LiveGameMenberDatas[i].CurrentGameParticipant.perks.perkStyle}BG", typeof(Sprite));
            }
        }
        return sprite;
    }

    public Sprite SetSubRuneBackGround(string summonerName)
    {
        Sprite sprite = Resources.Load<Sprite>("Perks/9999");

        for (int i = 0; i < LOM.Instance.LiveGameManager.LiveGameMenberDatas.Count; i++)
        {
            if (LOM.Instance.LiveGameManager.LiveGameMenberDatas[i].SummonerName + "(" + LOM.Instance.RiotIDDataManager.ChampionID[(int)LOM.Instance.LiveGameManager.LiveGameMenberDatas[i].ChampionID] + ")" == summonerName)
            {
                if (LOM.Instance.LiveGameManager.LiveGameMenberDatas[i].MatchDtos.Count == 0) return sprite;

                sprite = (Sprite)Resources.Load($"Perks/{LOM.Instance.LiveGameManager.LiveGameMenberDatas[i].CurrentGameParticipant.perks.perkSubStyle}BG", typeof(Sprite));
            }
        }
        return sprite;
    }

    public Sprite SetChampionIcon(string summonerName)
    {
        Sprite sprite = Resources.Load<Sprite>("Icon/9999");

        for (int i = 0; i < LOM.Instance.LiveGameManager.LiveGameMenberDatas.Count; i++)
        {
            if (LOM.Instance.LiveGameManager.LiveGameMenberDatas[i].SummonerName + "(" + LOM.Instance.RiotIDDataManager.ChampionID[(int)LOM.Instance.LiveGameManager.LiveGameMenberDatas[i].ChampionID] + ")" == summonerName)
            {
                if (LOM.Instance.LiveGameManager.LiveGameMenberDatas[i].MatchDtos.Count == 0) return sprite;
                Debug.Log(LOM.Instance.LiveGameManager.LiveGameMenberDatas[i].CurrentGameParticipant.championId);
                sprite = (Sprite)Resources.Load($"Icon/{LOM.Instance.LiveGameManager.LiveGameMenberDatas[i].CurrentGameParticipant.championId}", typeof(Sprite));
            }
        }
        return sprite;
    }
}
