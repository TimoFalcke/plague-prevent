using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RuleEntry : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI ruleText;
    [SerializeField] TextMeshProUGUI ruleCost;
    [SerializeField] TextMeshProUGUI decisionType;
    [SerializeField] Button button;

    Rule rule;
    
    public void Initialize(Rule rule)
    {
        this.rule = rule;

        if (rule.cost > StatsController.Instance.money)// || rule.acceptanceTreshold > StatsController.Instance.approval)
            button.interactable = false;

        ruleText.text = rule.text;
        ruleCost.text = rule.cost.ToString();

        switch (rule.decisionType)
        {
            case DecisionType.recommendation:
                decisionType.text = "Empfehlung";
                break;

            case DecisionType.law:
                decisionType.text = "Gesetz";
                break;
        }
    }

    internal void Disable()
    {
        button.interactable = false;
    }

    public void ButtonPressed()
    {
        RuleManager.Instance.EnactRule(rule);
        UIController.Instance.HideRuleScreen();
        
    }
}
