using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class InfectionStateMachine : MonoBehaviour
{
    public Agent Agent;

    //Chance for any Agent to become a severe case => Hospitalization
    [SerializeField] public float SeverityRate = 0.2f;
    //Chance for any severe Agent to die from the disease
    [SerializeField] public float DeathRate = 0.05f;
    
    private void Awake()
    {
        Agent = GetComponent<Agent>();
    }

    /// <summary>
    /// Infects a healthy agent. Ignores others.
    /// </summary>
    public void OnContact()
    {
        if (Agent.Status == InfectionStatus.HEALTHY)
        {
            Agent.Status = InfectionStatus.CARRIER;
        }
    }

    /// <summary>
    /// Advances Agent to the next infection status.
    /// </summary>
    public void OnTimeStep()
    {
        switch (Agent.Status)
        {
            case InfectionStatus.CARRIER:
                Agent.Status = DetermineSeverity();
                break;
            case InfectionStatus.SICK:
                Agent.Status = InfectionStatus.IMMUNE;
                break;
            case InfectionStatus.SEVERE:
                Agent.Status = DetermineDeath();
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// Returns the Agent back to Healthy if they are currently a carrier.
    /// </summary>
    public void RemoveCarrier()
    {
        if (Agent.Status == InfectionStatus.CARRIER)
        {
            Agent.Status = InfectionStatus.HEALTHY;
        }
    }

    private InfectionStatus DetermineSeverity()
    {
       return Random.Range(0.0f, 1.0f) <= SeverityRate ? InfectionStatus.SEVERE : InfectionStatus.SICK;
    }

    private InfectionStatus DetermineDeath()
    {
        return Random.Range(0.0f, 1.0f) <= DeathRate ? InfectionStatus.DEAD : InfectionStatus.IMMUNE;
    }
}
