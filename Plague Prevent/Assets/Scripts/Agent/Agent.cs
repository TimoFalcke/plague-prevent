using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour
{
    #region fields
    Gender _gender;
    Origin _origin;
    string _name;
    int _age;
    Profession _profession;
    InfectionStatus _status;
    [SerializeField]
    Location _home;
    Location _workplace;
    AgentSchedule schedule;
    AgentMovement _agentMovement;
    float _speed;
    #endregion

    private void Awake()
    {
        _gender = (Gender)Random.Range(0, Gender.GetValues(typeof(Gender)).Length);
        _origin = (Origin)Random.Range(0, Origin.GetValues(typeof(Origin)).Length);
        _name = Utilities.GenerateName(_gender, _origin);
        _age = Random.Range(AgentController.Instance.MinAge, AgentController.Instance.MaxAge);
        _profession = (Profession)Random.Range(0, Profession.GetValues(typeof(Profession)).Length);
        _status = InfectionStatus.HEALTHY;
        //_home = AgentController.Instance.GetHomeLocation();
        _speed = Random.Range(AgentController.Instance.MinSpeed, AgentController.Instance.MaxSpeed);

        _agentMovement = GetComponentInChildren<AgentMovement>();
        _agentMovement.Initilize(_home.Node);

        schedule = GetComponent<AgentSchedule>();
        schedule.Initialize(this);
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
    #endregion
}
