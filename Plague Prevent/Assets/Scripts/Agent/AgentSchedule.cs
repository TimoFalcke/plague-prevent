﻿using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class AgentSchedule : MonoBehaviour
{
    AgentScheduleElement[] schedule;
    [SerializeField] Transform routineContainer;

    [SerializeField] TextMeshPro decisionFeedback;

    Dictionary<AgentActivityType, List<Location>> routines;

    int currentScheduleElementIndex = 0;
    //float remainingScheduleTime;

    private InfectionStateMachine _infectionStateMachine;

    Location targetLocation;
    //int hourForNextSchedule;
    AgentMovement _agentMovement;

    private Agent _agent;

    private Location _hospital;

    AgentArchetype archetype;

    public Action<Location> OnNewTargetChosen;

    AgentScheduleElement CurrentScheduleElement
    {
        get {
            return schedule[currentScheduleElementIndex];
        }
    }
    //TODO: Remove this later
    private void Awake()
    {
        _infectionStateMachine = GetComponent<InfectionStateMachine>();
    }

    /// <summary>
    /// Initialize Routines
    /// </summary>
    /// <param name="agent"></param>
    public void Initialize(Agent agent, AgentArchetype archetype)
    {
        this.archetype = archetype;
        //AgentRoutine[] allRoutines = GetComponentsInChildren<AgentRoutine>();
        _agentMovement = GetComponentInChildren<AgentMovement>();
        _agent = GetComponent<Agent>();
        _hospital = LocationController.Instance.GetRandomHospital();
        //_infectionStateMachine = GetComponent<InfectionStateMachine>();

        // random routines
        routines = new Dictionary<AgentActivityType, List<Location>>()
        {
            { AgentActivityType.HOME, new List<Location>() { agent.Home } },
            { AgentActivityType.WORK, new List<Location>() { agent.Workplace } },
            { AgentActivityType.FREETIME, new List<Location>() {
                LocationController.Instance.GetRandomEntertainment(),
                LocationController.Instance.GetRandomEntertainment(),
                LocationController.Instance.GetRandomEntertainment(),
            } },
        };

        // archetype
        schedule = archetype.schedule;

        for (int i = 0; i < schedule.Length; i++)
        {
            if (schedule[i].LiesInTimeFrame(StatsController.Instance.currentDateTime.Hour))
            {
                currentScheduleElementIndex = i;
                break;
            }
        }

        //Debug.Log($" { archetype } at { StatsController.Instance.currentDateTime.Hour } o'clock: activity = { CurrentScheduleElement.activityType}");

        //hourForNextSchedule = CurrentScheduleElement.EndTime;

        #region old
        //AgentRoutine homeRoutine = routineContainer.gameObject.AddComponent<AgentRoutine>();
        //homeRoutine.Initialize(AgentActivityType.HOME, agent.Home, 1);

        //AgentRoutine workRoutine = routineContainer.gameObject.AddComponent<AgentRoutine>();
        //workRoutine.Initialize(AgentActivityType.WORK, agent.Workplace, 1);

        //routines = new Dictionary<AgentActivityType, List<AgentRoutine>>()
        //{
        //    { AgentActivityType.HOME, new List<AgentRoutine>() { homeRoutine } },

        //};

        // Add general routines
        //foreach (AgentRoutine routine in allRoutines)
        //{
        //    //   if (!routines.ContainsKey(routine.activityType))
        //    routines.Add(routine.activityType, new List<AgentRoutine>());

        //    routines[routine.activityType].Add(routine);
        //}


        //// Create Home Routine if none is defined
        //if (!routines.ContainsKey(AgentActivityType.HOME))
        //{
        //    AgentRoutine homeRoutine = routineContainer.gameObject.AddComponent<AgentRoutine>();
        //    homeRoutine.Initialize(AgentActivityType.HOME, agent.Home, 1);
        //    routines.Add(AgentActivityType.HOME, new List<AgentRoutine>() { homeRoutine });
        //}

        //// Create Work Routine if none is defined
        //if (!routines.ContainsKey(AgentActivityType.WORK))
        //{
        //    AgentRoutine workRoutine = routineContainer.gameObject.AddComponent<AgentRoutine>();
        //    workRoutine.Initialize(AgentActivityType.WORK, agent.Workplace, 1);
        //    routines.Add(AgentActivityType.WORK, new List<AgentRoutine>() { workRoutine });
        //}
        #endregion

        DecideRoutine();
    }

    void DecisionFeedback()
    {
        if (_agent.Status == InfectionStatus.SEVERE)
        {
            decisionFeedback.text = "X";
        }
        switch (CurrentScheduleElement.activityType)
        {
            case AgentActivityType.FREETIME:
                decisionFeedback.text = "F";
                break;

            case AgentActivityType.HOME:
                decisionFeedback.text = "H";
                break;

            case AgentActivityType.WORK:
                decisionFeedback.text = "W";
                break;
            case AgentActivityType.HOSPITAL:
                decisionFeedback.text = "X";
                break;
        }

        decisionFeedback.transform.localScale = Vector3.one;
        decisionFeedback.DOColor(Color.white, 0.1f);
        decisionFeedback.gameObject.SetActive(true);
        decisionFeedback.transform.DOLocalMoveY(2.7f, 1f).From(0).SetEase(Ease.InOutSine).OnComplete(
            () => decisionFeedback.DOColor(Color.clear, 0.5f));
        decisionFeedback.transform.DOScale(2, 1f);


    }

    private void Update()
    {
        //remainingScheduleTime -= Time.deltaTime * StatsController.Instance.hoursPerSecond;


        if (!CurrentScheduleElement.LiesInTimeFrame(StatsController.Instance.currentDateTime.Hour))
            NextSchedule();
            
        //if (remainingScheduleTime <= 0)

    }

    /// <summary>
    /// Go to next schedule element in agent schedule
    /// </summary>
    void NextSchedule()
    {
        currentScheduleElementIndex = (currentScheduleElementIndex + 1) % schedule.Length;

        //Debug.Log($" { archetype } at { StatsController.Instance.currentDateTime.Hour } o'clock: activity = { CurrentScheduleElement.activityType}");

        //remainingScheduleTime += CurrentScheduleElement.Duration;
        DecideRoutine();
        // DecisionFeedback();
    }

    /// <summary>
    /// Decide which activity to do in this schedule
    /// </summary>
    void DecideRoutine()
    {
        if (_agent.Status == InfectionStatus.SEVERE)
        {
            targetLocation = _hospital;
        }
        else
        {
            targetLocation = routines[CurrentScheduleElement.activityType]
                [UnityEngine.Random.Range(0, routines[CurrentScheduleElement.activityType].Count)];

            AgentActivityType plannedActivity = CurrentScheduleElement.activityType;
            if (plannedActivity != AgentActivityType.HOME)
            {
                // decide to go home instead
                float routineProbability = 1;

                switch (plannedActivity)
                {
                    case AgentActivityType.WORK: routineProbability *= StatsController.Instance.goToWorkProbabilityGlobal; break;
                    case AgentActivityType.FREETIME: routineProbability *= StatsController.Instance.goToEntertainmentProbabilityGlobal; break;
                }

                routineProbability *= StatsController.Instance.visitProbability[targetLocation.LocationType];

                if (UnityEngine.Random.value > routineProbability)
                {
                    Debug.Log(this + " decides to go / stay home instead of going to " + targetLocation);
                    targetLocation = routines[AgentActivityType.HOME][0];
                }
            }
        }


        OnNewTargetChosen?.Invoke(targetLocation);
        _agentMovement.SetPath(targetLocation.Node);
    }
}

[System.Serializable]
public class AgentScheduleElement
{
    public AgentActivityType activityType;
    public Vector2 time;

    public float Duration
    {
        get {
            return time.y - time.x;
        }
    }

    public int EndTime
    {
        get {
            return Mathf.RoundToInt(time.y);
        }
    }

    public bool LiesInTimeFrame(int hour)
    {
        // night activity
        if (time.y < time.x)
        {
            return (hour < time.y || hour > time.x);
        }

        return (hour >= time.x && hour <= time.y);
    }
}

