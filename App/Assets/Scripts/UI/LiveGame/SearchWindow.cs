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
    [SerializeField]
    private Toggle _nameToggle = null;

    private SaveData _saveData = null;

    private void Awake()
    {
        _tagLineField.text = "JP1";
        _searchButton.onClick.AddListener(() => StartSearch());
        gameObject.SetActive(true);
        _saveData = new SaveData();
        _summonerField.onValueChanged.AddListener((value) => NameChange(value));

       
    }

    public void StartSearch()
    {
        if (_summonerField.text.Length == 0 || _summonerField.text == null)
        {
            _errorText.text = "サモナーネームの入力値が不適切です";
            return;
        }
        if (_tagLineField.text.Length == 0 || _tagLineField.text == null)
        {
            _errorText.text = "タグラインの入力値が不適切です";
            return;
        }

        if (_nameToggle.isOn)
        {
            LOM.Instance.SaveData.Save(_saveData);
        }
        else
        {

            _saveData.SummonerName = null;
            LOM.Instance.SaveData.Save(_saveData);
        }

        LOM.Instance.LiveGameManager.SetSummonerName(_summonerField.text);
        LOM.Instance.LiveGameManager.SetTagLine(_tagLineField.text);
        LOM.Instance.LiveGameManager.IsSearch = true;
        gameObject.SetActive(false);
    }

    public void NameChange(string name)
    {
        _saveData.SummonerName = name;
    }

    private void OnEnable()
    {
        _summonerField.text = null;
        if (!(LOM.Instance.SaveData.Load().SummonerName == null))
        {
            _summonerField.text = LOM.Instance.SaveData.Load().SummonerName;
        }
    }
}
