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
    Location _home;
    #endregion

    private void Awake()
    {
        _gender = (Gender)Random.Range(0, Gender.GetValues(typeof(Gender)).Length);
        _origin = (Origin)Random.Range(0, Origin.GetValues(typeof(Origin)).Length);
        _name = Utilities.GenerateName(_gender, _origin);
        _age = Random.Range(AgentController.Instance.MinAge, AgentController.Instance.MaxAge);
        _profession = (Profession)Random.Range(0, Profession.GetValues(typeof(Profession)).Length);
        _status = InfectionStatus.NONE;
        _home = AgentController.Instance.GetHomeLocation();
    }

    #region properties
    public string Name { get => _name; }
    public int Age { get => _age; }
    public Profession Profession { get => _profession; }
    public InfectionStatus Status { get => _status; }
    public Location Home { get => _home; }
    public Origin Origin { get => _origin; }
    #endregion
}
