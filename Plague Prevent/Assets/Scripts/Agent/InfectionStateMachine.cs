using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class InfectionStateMachine : MonoBehaviour
{
    private Agent _agent;
    private SpriteRenderer _renderer;
    /*
    public Sprite healthy;
    public Sprite carrier;
    public Sprite infected;
    public Sprite severe;
    public Sprite immune; */

    //Chance for any Agent to become a severe case => Hospitalization
    [SerializeField] public float SeverityRate = 0.1f;
    //Chance for any severe Agent to die from the disease
    public float DeathRate = 0.8f;

    public int infection_overall = 0;
    private float infectRate;

    public int disease_overall = 0;
    private float diseaseRate;
    private bool turns = false; // Disable multiple time step calls for this agent
    private bool recovers = false;

    public int recovery_overall = 0;
    private float recoveryRate;
    
    private void Awake()
    {
        _agent = GetComponent<Agent>();
        _renderer = GetComponentInChildren<SpriteRenderer>();
        infectRate = Random.Range(0.2f, 0.3f);  //Balance this
        diseaseRate = Random.Range(0.2f, 0.4f); //Balance this
        recoveryRate = Random.Range(0.5f, 0.7f);//Balance this
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
                SoundController.Instance.PlayAgentInfected1();
                StartCoroutine(nameof(DelayCarrierSprite));
            }
             
        }
    }

    /// <summary>
    /// Advances Agent to the next infection status.
    /// </summary>
    /// DEPRECATED
    public void OnTimeStep()
    {
        switch (_agent.Status)
        {
            case InfectionStatus.CARRIER:
                if (!turns)
                {
                    DetermineSeverity();
                }
                break;
            case InfectionStatus.SICK:
                if (!recovers)
                {
                    StartCoroutine(nameof(DelayRecoverySprite));
                }
                //StatsController.Instance.RemoveInfected();
                break;
            case InfectionStatus.SEVERE:
                DetermineDeath();
                if (_agent.Status == InfectionStatus.DEAD)
                {
                    _agent.SetInactive();
                }
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
        if (_agent.Status != InfectionStatus.CARRIER) return;
        if (Random.Range(0.0f, 1.0f) <= StatsController.Instance.handWashProbabilityPerHour)
        {
            _agent.Status = InfectionStatus.HEALTHY;
            _agent.AgentVisual.SetState(AgentVisual.SpriteStatus.HEALTHY);
        }
    }

    private void DetermineSeverity()
    {
        if (Random.Range(0.0f, 1.0f) <= SeverityRate) 
        {
           StartCoroutine(nameof(DelaySevere));
        }
        else
        {
            StartCoroutine(nameof(DelayInfection));
        }
    }

    private void DetermineDeath()
    {
        if (Random.Range(0.0f, 1.0f) <= DeathRate)
        {
            SoundController.Instance.PlayAgentDeath();
            _agent.Status = InfectionStatus.DEAD;
            _agent.SetInactive();
        }
        else
        {
            StartCoroutine(nameof(DelayRecoverySprite));
            //StatsController.Instance.RemoveInfected();
        }
    }

    private IEnumerator DelayInfection()
    {
        _agent.Status = InfectionStatus.SICK;
        _agent.AgentVisual.SetState(AgentVisual.SpriteStatus.INFECTED);
        SoundController.Instance.PlayAgentInfected2();
        turns = true;
        for (int i = 0; i < 100; i++)
        {
            disease_overall += 1;
            yield return new WaitForSeconds(diseaseRate);
        }
        
        StartCoroutine(nameof(DelayRecoverySprite));
        //StatsController.Instance.AddInfected();
    }

    private IEnumerator DelaySevere()
    {
        _agent.Status = InfectionStatus.SEVERE;
        _agent.AgentVisual.SetState(AgentVisual.SpriteStatus.SEVERE);
        turns = true;
        for (int i = 0; i < 100; i++)
        {
            disease_overall += 1;
            yield return new WaitForSeconds(diseaseRate);
        }
        DetermineDeath();
        // StatsController.Instance.AddInfected();
    }

    private IEnumerator DelayCarrierSprite()
    {
        _agent.AgentVisual.SetState(AgentVisual.SpriteStatus.CARRIER);
        for (int i = 0; i < 100; i++)
        {
            infection_overall += 1;
            yield return new WaitForSeconds(infectRate);
        }
        DetermineSeverity();
    }

    private IEnumerator DelayRecoverySprite()
    {
        for (int i = 0; i < 100; i++)
        {
            recovery_overall += 1;
            yield return new WaitForSeconds(recoveryRate);
        }
        _agent.AgentVisual.SetState(AgentVisual.SpriteStatus.IMUNE);
        _agent.Status = InfectionStatus.IMMUNE;
    }

    public void infectImmediately()
    {
        infection_overall = 100;
        disease_overall = 100;
        _agent.Status = InfectionStatus.SICK;
        _agent.AgentVisual.SetState(AgentVisual.SpriteStatus.INFECTED);
        StartCoroutine(nameof(WaitForFirstInfected));
    }

    private IEnumerator WaitForFirstInfected()
    {
        while (!StatsController.Instance.GameStarted)
        {
            yield return new WaitForSeconds(1);
        }
        StartCoroutine(nameof(DelayRecoverySprite));
    }
}
