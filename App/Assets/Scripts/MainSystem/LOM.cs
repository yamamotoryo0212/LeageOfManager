using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LOM : SingletonMonoBehaviour<LOM>
{
    [SerializeField]
    private RiotAPIData _mainSystem = null;
    public RiotAPIData Mainsystem
    {
        get { return _mainSystem; }
    }

    [SerializeField]
    private UserData _userData = null;
    public UserData UserData
    {
        get { return _userData; }
    }
}
