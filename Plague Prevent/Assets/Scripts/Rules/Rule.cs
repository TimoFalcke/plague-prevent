using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Rule : ScriptableObject
{
    [Header("Display")]
    [Multiline, TextArea]
    public string text;
    public DecisionType decisionType;

    [Header("Enforcing Conditions")]
    public float acceptanceTreshold = 1;
    public float cost = 1;
    [Tooltip("How often can the rule be shown?")]
    public int uses = 1;

    [Header("Rules")]
    public GeneralRule[] generalRules;
    public LocationRule[] locationRules;
    
}

public enum DecisionType
{
    recommendation,
    law,
    action
}

[System.Serializable]
public struct LocationRule
{
    public LocationRuleType ruleType;
    public LocationType targetLocation;
    public float value;
}

[System.Serializable]
public struct GeneralRule
{
    public GeneralRuleType ruleType;
    public float value;
}
