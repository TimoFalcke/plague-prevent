using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

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

    [SerializeField]
    GameObject _agentPrefab;
    [SerializeField]
    GameObject _agentContainer;
    [SerializeField]
    AgentArchetype[] archetypes;

    List<Agent> _agents;
    List<AgentArchetype> possibleArchetypes;
    #endregion

    #region initilization
    private void Awake()
    {
        GetInstance();
        _agents = new List<Agent>();
        possibleArchetypes = new List<AgentArchetype>();
        foreach (AgentArchetype archetype in archetypes)
        {
            for (int i = 0; i < archetype.frequency; i++)
                possibleArchetypes.Add(archetype);
        }
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

    public void InfectRandom()
    {
        _agents[Random.Range(0, _agents.Count)].Infect();
    }

    public Location GetHomeLocation()
    {
        // needs to be implemented
        return null;
    }

    public AgentArchetype GetArchetype()
    {

        return possibleArchetypes[Random.Range(0, possibleArchetypes.Count)];
    }

    public int RegisterAgent(Agent agent)
    {
        _agents.Add(agent);
        return (_agents.Count - 1);
    }
    public void UnregisterAgent(Agent agent)
    {
        _agents.Remove(agent);
    }

    #endregion

    #region properties
    public static AgentController Instance { get => _instance; }
    public int MinAge { get => _minAge; }
    public int MaxAge { get => _maxAge;  }
    public float MinSpeed { get => _minSpeed; }
    public float MaxSpeed { get => _maxSpeed;}
    public GameObject AgentPrefab { get => _agentPrefab; }
    public GameObject AgentContainer { get => _agentContainer; }
    public List<Agent> Agents { get => _agents;  }
    #endregion
}
