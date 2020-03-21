using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RuleEntry : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI ruleText;
    [SerializeField] Button button;

    Rule rule;
    
    public void Initialize(Rule rule)
    {
        this.rule = rule;

        ruleText.text = rule.text;        
    }

    internal void Disable()
    {
        button.interactable = false;
    }

    public void ButtonPressed()
    {
        UIController.Instance.HideRuleScreen();
        
    }
}
