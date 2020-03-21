using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsController : Controller
{
    #region fields
    private static StatsController _instance;
    #endregion

    #region initilization
    private void Awake()
    {
        GetInstance();
    }
    #endregion

    #region methods
    public StatsController GetInstance()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        
        return _instance;
    }
    #endregion

    #region properties
    public static StatsController Instance { get => _instance; }
    #endregion
}
