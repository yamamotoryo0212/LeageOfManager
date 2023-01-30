using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LOM : SingletonMonoBehaviour<LOM>
{
    [SerializeField]
    private DevelopmentData _mainSystem = null;
    public DevelopmentData Mainsystem
    {
        get { return _mainSystem; }
    }

    [SerializeField]
    private UserData _userData = null;
    public UserData UserData
    {
        get { return _userData; }
    }

    [SerializeField]
    private LiveGameManager _liveGameManager = null;
    public LiveGameManager LiveGameManager
    {
        get { return _liveGameManager; }
    }
}
