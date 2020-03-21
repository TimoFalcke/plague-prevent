using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsController : Controller
{
    #region fields
    private static StatsController _instance;

    [Tooltip("How many ingame hours pass in one second game time")]
    public float hoursPerSecond = 1;

    public int infectedCount;
    public int deathCount;
    [SerializeField]public int population;
    [SerializeField]public int approval;
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

    public void AddInfected()
    {
        infectedCount += 1;
    }

    public void AddDeath()
    {
        //Add death to counter AND remove one person from overall population
        deathCount += 1;
        population -= 1;
    }

    public void ChangeApproval(int change)
    {
        approval += change;
    }
    
    #endregion

    #region properties
    public static StatsController Instance { get => _instance; }
    #endregion
}
