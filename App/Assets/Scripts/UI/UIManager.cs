using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private LiveGameUIManager _liveGameUIManager = null;
    public LiveGameUIManager LiveGameUIManager
    {
        get { return _liveGameUIManager; }
    }
}
