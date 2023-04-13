using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SummonerData : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _summonerName = null;
    public TextMeshProUGUI SummonerName
    {
        get { return _summonerName; }
    }
    [SerializeField]
    private TextMeshProUGUI _summonerLevel = null;
    public TextMeshProUGUI SummonerLevel
    {
        get { return _summonerLevel; }
    }
    [SerializeField]
    private Image _championIcon = null;
    public Image ChampionIcon
    {
        get { return _championIcon; }
    }
    [SerializeField]
    private List<Image> _summonerSpellIcon = new List<Image>();
    public List<Image> SummonerSpellIcon
    {
        get { return _summonerSpellIcon; }
    }
    [SerializeField]
    private Image _rankIcon = null;
    public Image RankIcon
    {
        get { return _rankIcon; }
    }
    [SerializeField]
    private TextMeshProUGUI _rankText = null;
    public TextMeshProUGUI RankText
    {
        get { return _rankText; }
    }
    [SerializeField]
    private TextMeshProUGUI _winRateText = null;
    public TextMeshProUGUI WinRateText
    {
        get { return _winRateText; }
    }
    [SerializeField]
    private TextMeshProUGUI _rankWinRateText = null;
    public TextMeshProUGUI RankWinRateText
    {
        get { return _rankWinRateText; }
    }
    [SerializeField]
    private TextMeshProUGUI _kDARateText = null;
    public TextMeshProUGUI KDARateText
    {
        get { return _kDARateText; }
    }
    [SerializeField]
    private List<Image> _favoriteChampionIcon = new List<Image>();
    public List<Image> FavoriteChampionIcon
    {
        get { return _favoriteChampionIcon; }
    }
}
