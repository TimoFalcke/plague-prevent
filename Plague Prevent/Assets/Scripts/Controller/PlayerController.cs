using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region fields
    private static PlayerController _instance;
    #endregion

    #region initilization
    private void Awake()
    {
        GetInstance();
    }
    #endregion

    #region methods
    public PlayerController GetInstance()
    {
        if (_instance == null)
        {
            _instance = this;
        }

        return _instance;
    }
    #endregion

    #region properties
    public static PlayerController Instance { get => _instance; }
    #endregion
}
