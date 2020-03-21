using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentRoutine : MonoBehaviour
{
    public AgentActivityType activityType;
    public Location targetLocation;
    public float probability;

    public void Initialize(AgentActivityType activityType, Location targetLocation, float probability)
    {
        this.activityType = activityType;
        this.targetLocation = targetLocation;
        this.probability = probability;
    }
}
