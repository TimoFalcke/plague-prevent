using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class AgentArchetype : ScriptableObject
{
    [Multiline, TextArea]
    [SerializeField] string description;

    public int frequency;

    public AgentScheduleElement[] schedule;
    public LocationType[] possibleWorkplaces;
    public LocationType[] possibleEntertainments;
    
}
