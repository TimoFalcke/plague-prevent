using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Rule : ScriptableObject
{
    [Multiline, TextArea]
    public string text;

    [Header("Values")]
    public float acceptance = -1;
    public float cost = 1;


    [Header("Rule Definition")]
    public GeneralRuleType generalRule;

    [Space]
    public LocationRuleType locationRule;
    public LocationType targetLocation;

    [Space]
    public float value = 1;
}
