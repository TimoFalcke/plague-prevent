using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Sprites;
using UnityEngine.Rendering;

public class Agent : MonoBehaviour
{
    #region fields
    int _ID;
    Gender _gender;
    Origin _origin;
    string _name;
    int _age;
    Profession _profession;
    [SerializeField]
    InfectionStatus _status;
    Location _home;
    Location _workplace;
    Location _currrentLocation;
    AgentSchedule schedule;
    AgentMovement _agentMovement;
    AgentArchetype archetype;
    float _speed;
    [SerializeField] private float _infectivity = 0.5f;
    bool _hasLocation;
    [SerializeField]
    AgentVisual _agentVisual;
    InfectionStateMachine _infectionStateMachine;
    SortingGroup _sortingGroup;
    #endregion

    private void Awake()
    {
        _sortingGroup = GetComponent<SortingGroup>();
        _infectionStateMachine = GetComponent<InfectionStateMachine>();
        _gender = (Gender)UnityEngine.Random.Range(0, Gender.GetValues(typeof(Gender)).Length);
        _origin = (Origin)UnityEngine.Random.Range(0, Origin.GetValues(typeof(Origin)).Length);
        _name = Utilities.GenerateName(_gender, _origin);
        _age = UnityEngine.Random.Range(AgentController.Instance.MinAge, AgentController.Instance.MaxAge);
        _profession = (Profession)UnityEngine.Random.Range(0, Profession.GetValues(typeof(Profession)).Length);
        _status = InfectionStatus.HEALTHY;
        _speed = UnityEngine.Random.Range(AgentController.Instance.MinSpeed, AgentController.Instance.MaxSpeed);
    }

    internal void Infect()
    {
        GetComponent<InfectionStateMachine>().infectImmediately();
    }

    public void Initilize(Location home)
    {
        _ID = AgentController.Instance.RegisterAgent(this);
        _sortingGroup.sortingOrder = _ID;
        archetype = AgentController.Instance.GetArchetype();


        _home = home;
        SetCurrentLocation(_home);
        _workplace = LocationController.Instance.GetRandomWorkplace();
        _agentMovement = GetComponentInChildren<AgentMovement>();
        _agentMovement.Initilize(_home.Node);


        schedule = GetComponent<AgentSchedule>();
        schedule.Initialize(this, archetype);

    }

    public void SetCurrentLocation(Location loc)
    {
        loc.RegisterAgent(this);
        _currrentLocation = loc;
        _hasLocation = true;
    }

    public void UnsetLocation()
    {
      if (_hasLocation == true)
        {
            _currrentLocation.Unregister(this);
            _currrentLocation = null;
            _hasLocation = false;
        }
    }

    public void SetInactive()
    {
        UnsetLocation();
        this.gameObject.SetActive(false);
        //StatsController.Instance.AddToDeathCounter();
        //AgentController.Instance.UnregisterAgent(this);
    }

    #region properties
    public string Name { get => _name; }
    public int Age { get => _age; }
    public Profession Profession { get => _profession; }
    public InfectionStatus Status { get => _status;
        set => _status = value;
    }
    public Location Home { get => _home; }

    public Location Workplace { get => _workplace; }
    public Origin Origin { get => _origin; }
    public float Speed { get => _speed; }

    public float Infectivity
    {
        get 
        { 
            return HasLocation ? StatsController.Instance.spreadVirusProbability[CurrrentLocation.LocationType] 
                : StatsController.Instance.spreadVirusProbabilityGlobal;
        }
    }
    public Location CurrrentLocation { get => _currrentLocation; }
    public bool HasLocation { get => _hasLocation;  }

    public AgentVisual AgentVisual => _agentVisual;

    public InfectionStateMachine InfectionStateMachine { get => _infectionStateMachine;  }

    #endregion
}
