using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class InfectionStateMachine : MonoBehaviour
{
    private Agent _agent;
    private SpriteRenderer _renderer;

    public Sprite healthy;
    public Sprite carrier;
    public Sprite infected;
    public Sprite severe;
    public Sprite immune;

    //Chance for any Agent to become a severe case => Hospitalization
    [SerializeField] public float SeverityRate = 0.2f;
    //Chance for any severe Agent to die from the disease
    [SerializeField] public float DeathRate = 0.05f;
    
    private void Awake()
    {
        _agent = GetComponent<Agent>();
        _renderer = GetComponentInChildren<SpriteRenderer>();
    }

    /// <summary>
    /// Infects a healthy agent. Ignores others.
    /// </summary>
    public void OnContact(float probability)
    {
        if (_agent.Status == InfectionStatus.HEALTHY)
        {
            if (Random.Range(0.0f, 1.0f) <= probability)
            {
                _agent.Status = InfectionStatus.CARRIER;
                _renderer.sprite = carrier;
            }
             
        }
    }

    /// <summary>
    /// Advances Agent to the next infection status.
    /// </summary>
    public void OnTimeStep()
    {
        switch (_agent.Status)
        {
            case InfectionStatus.CARRIER:
                _agent.Status = DetermineSeverity();
                break;
            case InfectionStatus.SICK:
                _agent.Status = InfectionStatus.IMMUNE;
                _renderer.sprite = immune;
                break;
            case InfectionStatus.SEVERE:
                _agent.Status = DetermineDeath();
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
        if (_agent.Status == InfectionStatus.CARRIER)
        {
            _agent.Status = InfectionStatus.HEALTHY;
        }
    }

    private InfectionStatus DetermineSeverity()
    {
       if (Random.Range(0.0f, 1.0f) <= SeverityRate)
       {
           _renderer.sprite = severe;
           return InfectionStatus.SEVERE;
       }
       else
       {
           _renderer.sprite = infected;
           return InfectionStatus.SICK;
       }
    }

    private InfectionStatus DetermineDeath()
    {
        if (Random.Range(0.0f, 1.0f) <= DeathRate)
        {
            //TODO: Change to death
            _renderer.sprite = healthy;
            return InfectionStatus.DEAD;
        }
        else
        {
            _renderer.sprite = immune;
            return InfectionStatus.IMMUNE;
        }
    }
}
