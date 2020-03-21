using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsController : Controller
{
    #region fields
    private static StatsController _instance;

    [Header("Time Flow")]
    [Tooltip("How many ingame hours pass in one second game time")]
    public float hoursPerSecond = 1;

    System.DateTime startDateTime = new System.DateTime(2020, 3, 1, 6, 0, 0);
    public System.DateTime currentDateTime;
    float worldTimer = 0;

    [Header("People")]
    public int infectedCount;
    public int deathCount;
    public float infectionProbability = 1.0f;
    public int population;
    
    [Header("Resources")]
    public int approval;
    public int currency;
    #endregion

    #region initilization
    private void Awake()
    {
        GetInstance();
        currentDateTime = startDateTime;
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

    private void Update()
    {
        float passedTime = Time.deltaTime * hoursPerSecond;
        worldTimer += passedTime;
        currentDateTime = startDateTime.AddHours(Mathf.Floor(worldTimer*4) / 4);
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

    public float InfectionProbability => infectionProbability;

    #endregion
}
