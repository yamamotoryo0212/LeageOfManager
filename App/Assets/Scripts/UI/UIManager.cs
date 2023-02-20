using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private LiveGameUIManager _liveGameUIManager = null;
    public LiveGameUIManager LiveGameUIManager
    {
        get { return _liveGameUIManager; }
    }

    [SerializeField]
    private Button _menuButton = null;
    [SerializeField]
    private Image _menuWindow = null;
    [SerializeField]
    private Button _closeWindow = null;

    private bool _ismenuEndPos = true;
    private bool _ismenuStartPos = true;
    private int _menuStartPos = -250;
    private int _menuEndPos = 150;
    private int _menuAnimSpeed = 60;

    private void Awake()
    {
        _closeWindow.gameObject.SetActive(false);
        _menuButton.onClick.AddListener(() => StartCoroutine(MenuWindowOpenAnim()));
        _closeWindow.onClick.AddListener(() => StartCoroutine(MenuWindowCloseAnim()));
    }

    private IEnumerator MenuWindowOpenAnim()
    {
        while (_ismenuEndPos)
        {
            Vector2 screenPos = RectTransformUtility.WorldToScreenPoint(_menuWindow.canvas.worldCamera, _menuWindow.rectTransform.position);
            Vector3 result = Vector3.zero;
            RectTransformUtility.ScreenPointToWorldPointInRectangle(_menuWindow.rectTransform, screenPos, _menuWindow.canvas.worldCamera, out result);

            if (result.x > _menuEndPos)
            {
                _ismenuEndPos = false;
                _ismenuStartPos = true;
                _closeWindow.gameObject.SetActive(true);
            }
            _menuWindow.transform.Translate(_menuAnimSpeed, 0, 0);
            yield return new WaitForSeconds(0.01f);
        }
    }

    private IEnumerator MenuWindowCloseAnim()
    {
        while (_ismenuStartPos)
        {
            Vector2 screenPos = RectTransformUtility.WorldToScreenPoint(_menuWindow.canvas.worldCamera, _menuWindow.rectTransform.position);
            Vector3 result = Vector3.zero;
            RectTransformUtility.ScreenPointToWorldPointInRectangle(_menuWindow.rectTransform, screenPos, _menuWindow.canvas.worldCamera, out result);

            if (result.x < _menuStartPos)
            {
                _ismenuStartPos = false;
                _ismenuEndPos = true;
                _closeWindow.gameObject.SetActive(false);
            }
            _menuWindow.transform.Translate(-_menuAnimSpeed, 0, 0);
            yield return new WaitForSeconds(0.01f);
        }
    }
}
