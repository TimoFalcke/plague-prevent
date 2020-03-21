using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Rule : ScriptableObject
{
    [Multiline, TextArea]
    public string text;

    [Header("Values")]
    public float minAcceptance = -1;
    public float cost = 1;
    [Tooltip("How often can the rule be shown?")]
    public int uses = 1;


    [Header("Rule Definition")]
    public GeneralRuleType generalRule;

    [Space]
    public LocationRuleType locationRule;
    public LocationType targetLocation;

    [Space]
    public float value = 1;

    [Header("Following Rules")]
    public Rule[] unlockedRules;
    
}
