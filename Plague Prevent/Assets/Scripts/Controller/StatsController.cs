using System;
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

    System.DateTime startDateTime;
    public System.DateTime currentDateTime;
    public int startHour = 9;
    float worldTimer = 0;

    private bool gameStarted = false;

    public bool GameStarted => gameStarted;

    [Header("People")]
    private int immunCount;
    private int population;
    private int healthy;
    private int carrierCount;
    private int infectedCount;
    private int deathCount;

    [Header("Resources")]
    public float approval;
    public float money;
    public float income;

    [Header("People Behaviour")]
    // spread infection
    public float spreadVirusProbabilityGlobal = 1.0f;
    public Dictionary<LocationType, float> spreadVirusProbability = new Dictionary<LocationType, float>()
    {
        { LocationType.CULTURE,     0.5f },
        { LocationType.FACTORY,     0.5f },
        { LocationType.GASTRONOMY,  0.5f },
        { LocationType.HOSPITAL,    0.5f },
        { LocationType.OFFICE,      0.5f },
        { LocationType.PARTY,       0.5f },
        { LocationType.RESIDENTAL,  0.1f },
        { LocationType.SHOPPING,    0.5f },
        { LocationType.SUPERMARKET, 0.5f }
    };
    public float handWashProbabilityPerHour = 0.08f;
    public float infectionProbability = 1.0f;

    // death rate
    public float hospitalDeathRate = 0.5f;
    public float infectedDeathRate = 0.7f;
    
    // routine probability
    public Dictionary<LocationType, float> visitProbability = new Dictionary<LocationType, float>()
    {
        { LocationType.CULTURE,     1 },
        { LocationType.FACTORY,     1 },
        { LocationType.GASTRONOMY,  1 },
        { LocationType.HOSPITAL,    1 },
        { LocationType.OFFICE,      1 },
        { LocationType.PARTY,       1 },
        { LocationType.RESIDENTAL,  1 },
        { LocationType.SHOPPING,    1 },
        { LocationType.SUPERMARKET, 1 }
    };
    public float goToWorkProbabilityGlobal = 1.0f;
    public float goToEntertainmentProbabilityGlobal = 1.0f;
    #endregion

    #region initilization
    private void Awake()
    {
        GetInstance();
        startDateTime = new System.DateTime(2020, 3, 1, startHour, 0, 0);

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


    public void UpdatePopulationCount()
    {
        population = AgentController.Instance.Agents.Count;
    }

    public void UpdateCounts()
    {
        int _tempImmunCount = 0;
        int _tempPopulation = 0;
        int _tempHealthy = 0;
        int _tempCarrierCount = 0;
        int _tempInfectedCount = 0;
        int _tempDeathCount = 0;

        for (int i = 0; i < AgentController.Instance.Agents.Count; i++)
        {
            switch (AgentController.Instance.Agents[i].Status)
            {
                case InfectionStatus.CARRIER:
                    _tempCarrierCount += 1;
                    gameStarted = true;
                    break;
                case InfectionStatus.IMMUNE:
                    _tempImmunCount += 1;
                    break;
                case InfectionStatus.HEALTHY:
                    _tempHealthy += 1;
                    break;
                case InfectionStatus.DEAD:
                    _tempDeathCount += 1;
                    break;
                case InfectionStatus.SEVERE:
                case InfectionStatus.SICK:
                    _tempInfectedCount += 1;
                    break;
            }
            ///_tempPopulation = +1;
        }

        immunCount = _tempImmunCount;
        //population = _tempPopulation;
        healthy = _tempHealthy;
        carrierCount = _tempCarrierCount;
        infectedCount = _tempInfectedCount;
        deathCount = _tempDeathCount;
    }

    private void CheckVictoryCondition()
    {
        if (gameStarted)
        {
            if (deathCount > 10)
            {
                UIController.Instance.ShowGameOverScreen();
            }
            int r = healthy + immunCount;
            if (population - r <= 10 && infectedCount == 0 && carrierCount == 0)
            {
                UIController.Instance.ShowVictoryScreen();
            }
        }
    }

    private void Update()
    {
        float passedTime = Time.deltaTime * hoursPerSecond;
        worldTimer += passedTime;
        currentDateTime = startDateTime.AddHours(Mathf.Floor(worldTimer*4) / 4);
        UpdateCounts();
        CheckVictoryCondition();
        money += income / 24f * Time.deltaTime * hoursPerSecond;
    }

    //public void AddInfected()
   // {
    //    infectedCount += 1;
    //}

    //public void RemoveInfected()
    //{
    //    infectedCount -= 1;
    //}

    //public void AddDeath()
    //{
     //   deathCount += 1;
   // }

    public void ChangeApproval(int change)
    {
        approval += change;
    }
    
    #endregion

    #region properties
    public static StatsController Instance { get => _instance; }

    public float InfectionProbability => infectionProbability;

    public int Immuns { get => immunCount;  }
   
    public int Healthy { get => healthy; }
    public int Carrier { get => carrierCount;  }
    public int Infected { get => infectedCount; }
    public int Death { get => deathCount;  }
    public int Population { get => population; set => population = value; }

    public float WorldTime { get => worldTimer; }

    #endregion
}
