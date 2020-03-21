using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuleManager : Controller
{
    #region fields
    private static RuleManager _instance;

    [SerializeField] int rulesToChoseFrom = 3;
    [SerializeField] float ruleInterval = 20f;
    [SerializeField] List<Rule> allRules;
    List<Rule> rulePool;
    float timer;
    #endregion

    #region initilization
    private void Awake()
    {
        GetInstance();
        rulePool = new List<Rule>(allRules);
        timer = ruleInterval;
    }
    #endregion

    #region methods
    public RuleManager GetInstance()
    {
        if (_instance == null)
        {
            _instance = this;
        }

        return _instance;
    }

    public void EnactRule(Rule ruleToEnact)
    {
        ruleToEnact.uses--;

        //if (ruleToEnact.uses <= 0)
        //    rulePool.Remove(ruleToEnact);

        foreach (Rule unlockedRule in ruleToEnact.unlockedRules)
        {
            if (!rulePool.Contains(unlockedRule))
                rulePool.Add(unlockedRule);
        }
    }

    Rule[] SelectRuleSet()
    {
        return null;
    }

    private void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            timer += ruleInterval;

        }
    }

    #endregion

    #region properties
    public static RuleManager Instance { get => _instance; }
    #endregion
}
