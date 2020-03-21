using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentController : Controller
{
    #region fields
    private static AgentController _instance;
    [SerializeField]
    int _minAge;
    [SerializeField]
    int _maxAge;
    [SerializeField]
    float _minSpeed;
    [SerializeField]
    float _maxSpeed;
    #endregion

    #region initilization
    private void Awake()
    {
        GetInstance();
    }
    #endregion

    #region methods
    public AgentController GetInstance()
    {
        if (_instance == null)
        {
            _instance = this;
        }

        return _instance;
    }

    public Location GetHomeLocation()
    {
        // needs to be implemented
        return null;
    }
    #endregion

    #region properties
    public static AgentController Instance { get => _instance; }
    public int MinAge { get => _minAge; }
    public int MaxAge { get => _maxAge;  }
    public float MinSpeed { get => _minSpeed; }
    public float MaxSpeed { get => _maxSpeed;}
    #endregion
}
