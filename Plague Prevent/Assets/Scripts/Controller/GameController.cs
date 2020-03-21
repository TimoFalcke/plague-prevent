using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    #region fields

    static GameController _instance;

    
    #endregion

    #region initilization
    private void Awake()
    {
        GetInstance();
    }
    void Start()
    {
       
    }
    #endregion

    #region methods
    void Update()
    {
        
    }
    public GameController GetInstance()
    {
        if (_instance == null)
        {
            _instance = this;
        }

        return _instance;
    }
    #endregion

    #region properties
    public static GameController Instance { get => _instance; }

    #endregion


}
