using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SearchWindow : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField _summonerField = null;
    [SerializeField]
    private TMP_InputField _tagLineField = null;
    [SerializeField]
    private Button _searchButton = null;

    private void Awake()
    {
        _tagLineField.text = "JP1";
        _searchButton.onClick.AddListener(() => StartSearch());
        gameObject.SetActive(true);
    }

    public void StartSearch()
    {
        LOM.Instance.LiveGameManager.SetSummonerName(_summonerField.text);
        LOM.Instance.LiveGameManager.SetTagLine(_tagLineField.text);
        LOM.Instance.LiveGameManager.IsSearch = true;
        gameObject.SetActive(false);
    }
}
