using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LiveGameUIManager : MonoBehaviour
{
    [SerializeField]
    private SummonerDropdown _summonerDropdown = null;
    public SummonerDropdown SummonerDropdown
    {
        get { return _summonerDropdown; }
    }

    [SerializeField]
    private RawImage _loadingWindow = null;
    public RawImage LoadingWindow
    {
        get { return _loadingWindow; }
    }

    [SerializeField]
    private Button _viewChangeButton = null;
    [SerializeField]
    private Image _searchWindow = null;
    public Image SearchWindow
    {
        get { return _searchWindow; }
    }
    private bool _isLoad = false;
    public bool IsLoad
    {
        get { return _isLoad; }
        set { _isLoad = value; }
    }
    private bool _isSearchWindow = false;
    public bool IsSearchWindow
    {
        get { return _isSearchWindow; }
        set { _isSearchWindow = value; }
    }

    private void Awake()
    {
        // _homeButton.onClick.AddListener(() => LOM.Instance.LiveGameManager.ResetButton());
        //_homeButton.gameObject.SetActive(false);
        _viewChangeButton.onClick.AddListener(() => LOM.Instance.UIManager.LiveGameChangeWindow(UIManager.LiveGameWindowMode.Public));
    }

    private void Update()
    {
        if (LOM.Instance.LiveGameManager.IsSearch && !_isSearchWindow)
        {
            _loadingWindow.gameObject.SetActive(true);
            _isSearchWindow = true;
        }
        if (LOM.Instance.LiveGameManager.IsMatchRequest && !_isLoad)
        {
            _loadingWindow.gameObject.SetActive(true);
            _viewChangeButton.gameObject.SetActive(true);
            StartCoroutine(FadeIn(_loadingWindow));
            _isLoad = true;
        }
    }

    private IEnumerator FadeIn(RawImage rawImage)
    {

        rawImage.gameObject.SetActive(true); // 画像をアクティブにする

        Color c = rawImage.color;
        c.a = 1f;
        rawImage.color = c;

        while (true)
        {
            yield return null;
            c.a -= 0.02f;
            rawImage.color = c; // 画像の不透明度を下げる

            if (c.a <= 0f) // 不透明度が0以下のとき
            {
                c.a = 0f;
                rawImage.color = c; // 不透明度を0
                break; // 繰り返し終了
            }
        }

        rawImage.gameObject.SetActive(false); // 画像を非アクティブにする

    }

    public void ResetWindow()
    {
        Color color = _loadingWindow.color;
        color.a = 100;
        _loadingWindow.gameObject.SetActive(true);
        _viewChangeButton.gameObject.SetActive(false);
        _loadingWindow.color = color;
    }

    public void SetMatchHistory(int dropDownValue)
    {
        bool once = false;
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
                if (!once)
                {
                    string pass_ = $"https://jp1.api.riotgames.com/lol/champion-mastery/v4/champion-masteries/by-summoner/{LOM.Instance.LiveGameManager.LiveGameMenberDatas[dropDownValue].SummonerID}/top?count=1&api_key={LOM.Instance.Mainsystem.DevelopmentAPIKey}";
                    LOM.Instance.LiveGameManager.SetChampionMastery(pass_, LOM.Instance.LiveGameManager.LiveGameMenberDatas[dropDownValue].Puuid);
                    once = true;
                }                
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
                str = $"直近{LOM.Instance.LiveGameManager.MatchCount}戦の勝率 : " +
                        winRate +
                        "%";
                //Debug.Log(LOM.Instance.LiveGameManager.LiveGameMenberDatas[i].SummonerName + " : " + LOM.Instance.LiveGameManager.LiveGameMenberDatas[i].CurrentGameParticipant.teamId);
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
        string str = $"直近{LOM.Instance.LiveGameManager.MatchCount}戦のKDA : ?/?/?";

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
                str = $"直近{LOM.Instance.LiveGameManager.MatchCount}戦のKDA : " +
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

    public string SetRankWinRate(string summonerName)
    {
        string winRate = "ランクの勝率 :  ?";
        for(int i = 0; i < LOM.Instance.LiveGameManager.LiveGameMenberDatas.Count; i++)
        {
            if (LOM.Instance.LiveGameManager.LiveGameMenberDatas[i].SummonerName + "(" + LOM.Instance.RiotIDDataManager.ChampionID[(int)LOM.Instance.LiveGameManager.LiveGameMenberDatas[i].ChampionID] + ")" == summonerName)
            {
                if (LOM.Instance.LiveGameManager.LiveGameMenberDatas[i].MatchDtos.Count == 0) return winRate;

                foreach (var item in LOM.Instance.LiveGameManager.LiveGameMenberDatas[i].LeagueEntryDTO.leagueEntryDTOs)
                {
                    if (item.queueType == "RANKED_SOLO_5x5")
                    {
                        winRate = $"ランクの勝率({item.wins + item.losses}戦) : "  + (((float)item.wins / ((float)item.wins + (float)item.losses)) * 100).ToString("N2") + "%";
                    }
                }
            }
        }
        return winRate;
    }

    public string SetRankText(string summonerName)
    {        
        string tier = "unrenk";
        string rank = "unrank";
        string lp = "?";
        string str = "unrank";

        for (int i = 0; i < LOM.Instance.LiveGameManager.LiveGameMenberDatas.Count; i++)
        {
            if (LOM.Instance.LiveGameManager.LiveGameMenberDatas[i].SummonerName + "(" + LOM.Instance.RiotIDDataManager.ChampionID[(int)LOM.Instance.LiveGameManager.LiveGameMenberDatas[i].ChampionID] + ")" == summonerName)
            {
                if (LOM.Instance.LiveGameManager.LiveGameMenberDatas[i].MatchDtos.Count == 0) return rank;

                foreach (var item in LOM.Instance.LiveGameManager.LiveGameMenberDatas[i].LeagueEntryDTO.leagueEntryDTOs)
                {
                    if (item.queueType == "RANKED_SOLO_5x5")
                    {
                        tier = LOM.Instance.RiotIDDataManager.RankTierID[item.tier];
                        rank = item.rank;
                        lp = item.leaguePoints.ToString();

                        str = tier + rank + "\n" + lp + "LP";
                    }
                }
            }
        }
        return str;
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
                //Debug.Log(LOM.Instance.LiveGameManager.LiveGameMenberDatas[i].CurrentGameParticipant.championId);
                sprite = (Sprite)Resources.Load($"Icon/{LOM.Instance.LiveGameManager.LiveGameMenberDatas[i].CurrentGameParticipant.championId}", typeof(Sprite));
            }
        }
        return sprite;
    }

    public Sprite SetFavoriteChampionIcon(string summonerName)
    {
        Sprite sprite = Resources.Load<Sprite>("Icon/9999");

        for (int i = 0; i < LOM.Instance.LiveGameManager.LiveGameMenberDatas.Count; i++)
        {
            if (LOM.Instance.LiveGameManager.LiveGameMenberDatas[i].SummonerName + "(" + LOM.Instance.RiotIDDataManager.ChampionID[(int)LOM.Instance.LiveGameManager.LiveGameMenberDatas[i].ChampionID] + ")" == summonerName)
            {
                if (LOM.Instance.LiveGameManager.LiveGameMenberDatas[i].MatchDtos.Count == 0) return sprite;
                if (LOM.Instance.LiveGameManager.LiveGameMenberDatas[i].FavoriteChampion.Count == 0) return sprite;

                for (int j = 0; j < LOM.Instance.LiveGameManager.LiveGameMenberDatas[i].FavoriteChampion.Count; j++)
                {
                    sprite = (Sprite)Resources.Load($"Icon/{LOM.Instance.LiveGameManager.LiveGameMenberDatas[i].FavoriteChampion[j]}", typeof(Sprite));
                }                
            }
        }
        return sprite;
    }

    public Sprite SetRankIcon(string summonerName)
    {
        Sprite sprite = Resources.Load<Sprite>("RankEmblem/9999");

        for (int i = 0; i < LOM.Instance.LiveGameManager.LiveGameMenberDatas.Count; i++)
        {
            if (LOM.Instance.LiveGameManager.LiveGameMenberDatas[i].SummonerName + "(" + LOM.Instance.RiotIDDataManager.ChampionID[(int)LOM.Instance.LiveGameManager.LiveGameMenberDatas[i].ChampionID] + ")" == summonerName)
            {
                if (LOM.Instance.LiveGameManager.LiveGameMenberDatas[i].MatchDtos.Count == 0) return sprite;
                if (LOM.Instance.LiveGameManager.LiveGameMenberDatas[i].LeagueEntryDTO.leagueEntryDTOs.Count == 0)
                {
                    return sprite;
                }
                //Debug.Log(LOM.Instance.LiveGameManager.LiveGameMenberDatas[i].CurrentGameParticipant.championId);
                sprite = (Sprite)Resources.Load($"RankEmblem/{LOM.Instance.LiveGameManager.LiveGameMenberDatas[i].LeagueEntryDTO.leagueEntryDTOs[0].tier}", typeof(Sprite));
            }
        }
        return sprite;
    }
}
