using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Origin { GERMANY, ITALY, SPAIN }
public enum Gender { MALE, FEMALE }
public enum InfectionStatus { HEALTHY, CARRIER, IMMUNE, SICK, SEVERE, DEAD };
public enum Profession { WORKER };

public enum NodeType { LOCATION, STREET, INTERIOR }

public enum LocationType 
{ 
    RESIDENTAL, 
    OFFICE, 
    FACTORY,
    HOSPITAL,
    GASTRONOMY,
    PARTY,
    CULTURE,
    SUPERMARKET,
    SHOPPING
};
public enum AgentActivityType { HOME, WORK, FREETIME, HOSPITAL }


public enum GeneralRuleType 
{
    SpreadVirusProbability,
    WashHandsProbability,
    InfectionProbability,

    GoToEntertainmentProbability,
    GoToWorkProbability,

    Income,
    Acceptance,
    
    DeathRate,
    HospitalDeathRate,
    HospitalCapacity
}

public enum LocationRuleType
{
    None,
    VisitorProbability,
    SpreadVirusProbability
}