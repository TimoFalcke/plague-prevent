using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : Controller
{
    #region fields
    private static SoundController _instance;
    #endregion

    #region initilization
    private void Awake()
    {
        GetInstance();
    }
    #endregion

    #region methods
    public SoundController GetInstance()
    {
        if (_instance == null)
        {
            _instance = this;
        }

        return _instance;
    }
    #endregion

    #region properties
    public static SoundController Instance { get => _instance; }
    #endregion

}
