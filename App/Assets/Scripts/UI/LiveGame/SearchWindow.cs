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
    [SerializeField]
    private TextMeshProUGUI _errorText = null;

    private void Awake()
    {
        _tagLineField.text = "JP1";
        _searchButton.onClick.AddListener(() => StartSearch());
        gameObject.SetActive(true);
    }

    public void StartSearch()
    {
        if (_summonerField.text.Length == 0 || _summonerField.text == null)
        {
            _errorText.text = "�T���i�[�l�[���̓��͒l���s�K�؂ł�";
            return;
        }
        if (_tagLineField.text.Length == 0 || _tagLineField.text == null)
        {
            _errorText.text = "�^�O���C���̓��͒l���s�K�؂ł�";
            return;
        }

        LOM.Instance.LiveGameManager.SetSummonerName(_summonerField.text);
        LOM.Instance.LiveGameManager.SetTagLine(_tagLineField.text);
        LOM.Instance.LiveGameManager.IsSearch = true;
        gameObject.SetActive(false);
    }
}
