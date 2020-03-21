using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentSchedule : MonoBehaviour
{
    [SerializeField] AgentScheduleElement[] schedule;
    [SerializeField] Transform routineContainer;

    Dictionary<AgentActivityType, List<AgentRoutine>> routines;

    int currentScheduleElementIndex = 0;
    float remainingScheduleTime;

    AgentRoutine currentRoutine;
    AgentMovement _agentMovement;

    public Action<Location> OnNewTargetChosen;

    AgentScheduleElement CurrentScheduleElement
    {
        get {
            return schedule[currentScheduleElementIndex];
        }
    }

    /// <summary>
    /// Initialize Routines
    /// </summary>
    /// <param name="agent"></param>
    public void Initialize(Agent agent)
    {
        AgentRoutine[] allRoutines = GetComponentsInChildren<AgentRoutine>();
        _agentMovement = GetComponentInChildren<AgentMovement>();

        routines = new Dictionary<AgentActivityType, List<AgentRoutine>>();

        // Add general routines
        foreach (AgentRoutine routine in allRoutines)
        {
            if (!routines.ContainsKey(routine.activityType))
                routines.Add(routine.activityType, new List<AgentRoutine>());

            routines[routine.activityType].Add(routine);
        }

        // Create Home Routine if none is defined
        if (!routines.ContainsKey(AgentActivityType.HOME))
        {
            AgentRoutine homeRoutine = routineContainer.gameObject.AddComponent<AgentRoutine>();
            homeRoutine.Initialize(AgentActivityType.HOME, agent.Home, 1);
            routines.Add(AgentActivityType.HOME, new List<AgentRoutine>() { homeRoutine });
        }

        // Create Work Routine if none is defined
        if (!routines.ContainsKey(AgentActivityType.WORK))
        {
            AgentRoutine workRoutine = routineContainer.gameObject.AddComponent<AgentRoutine>();
            workRoutine.Initialize(AgentActivityType.WORK, agent.Workplace, 1);
            routines.Add(AgentActivityType.WORK, new List<AgentRoutine>() { workRoutine });
        }

        remainingScheduleTime = CurrentScheduleElement.Duration;
        DecideRoutine();
    }

    private void Update()
    {
        remainingScheduleTime -= Time.deltaTime * StatsController.Instance.hoursPerSecond;

        if (remainingScheduleTime <= 0)
            NextSchedule();
    }

    /// <summary>
    /// Go to next schedule element in agent schedule
    /// </summary>
    void NextSchedule()
    {
        currentScheduleElementIndex = (currentScheduleElementIndex + 1) % schedule.Length;
        remainingScheduleTime += CurrentScheduleElement.Duration;
        DecideRoutine();
    }

    /// <summary>
    /// Decide which activity to do in this schedule
    /// </summary>
    void DecideRoutine()
    {
        currentRoutine = routines[CurrentScheduleElement.activityType]
            [UnityEngine.Random.Range(0, routines[CurrentScheduleElement.activityType].Count)];

        OnNewTargetChosen?.Invoke(currentRoutine.targetLocation);
        _agentMovement.SetPath(currentRoutine.targetLocation.Node);


    }
}

[System.Serializable]
class AgentScheduleElement
{
    public AgentActivityType activityType;
    public Vector2 time;

    public float Duration
    {
        get {
            return time.y - time.x;
        }
    }
}

