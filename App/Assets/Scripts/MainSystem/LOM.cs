using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LOM : SingletonMonoBehaviour<LOM>
{
    [SerializeField]
    private MainSystem _mainSystem = null;
    public MainSystem Mainsystem
    {
        get { return _mainSystem; }
    }
}
