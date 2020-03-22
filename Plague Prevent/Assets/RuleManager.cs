using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RuleManager : Controller
{
    #region fields
    private static RuleManager _instance;

    [SerializeField] int ruleCountToChoseFrom = 4;
    [SerializeField] float ruleInterval = 20f;
    [SerializeField] float ruleStartTime = 5f;
    [SerializeField] List<Rule> allRules;
    List<Rule> rulePool;
    float timer;
    #endregion

    #region initilization
    private void Awake()
    {
        GetInstance();
        rulePool = new List<Rule>(allRules);
        timer = ruleStartTime;
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
        SoundController.Instance.PlayConfirm();


        ruleToEnact.uses--;

        if (ruleToEnact.uses <= 0)
           rulePool.Remove(ruleToEnact);

        StatsController.Instance.money -= ruleToEnact.cost;

        foreach (LocationRule locationRule in ruleToEnact.locationRules)
        {
            EnactLocationRule(locationRule);
        }
        foreach (GeneralRule generalRule in ruleToEnact.generalRules)
        {
            EnactGeneralRule(generalRule);
        }
    }

    void EnactLocationRule(LocationRule locationRule)
    {
        LocationType targetLocation = locationRule.targetLocation;
        float value = locationRule.value;

        switch (locationRule.ruleType)
        {
            case LocationRuleType.SpreadVirusProbability:
                StatsController.Instance.spreadVirusProbability[targetLocation] += value;
                break;

            case LocationRuleType.VisitorProbability:
                StatsController.Instance.visitProbability[targetLocation] += value;
                break;
        }
    }

    void EnactGeneralRule(GeneralRule generalRule)
    {
        float value = generalRule.value;

        switch (generalRule.ruleType)
        {
            case GeneralRuleType.Income:
                StatsController.Instance.income += value;
                break;

            case GeneralRuleType.Acceptance:
                StatsController.Instance.approval += value / 100f;
                break;

            case GeneralRuleType.DeathRate:
                StatsController.Instance.infectedDeathRate += value;
                break;

            case GeneralRuleType.GoToEntertainmentProbability:
                StatsController.Instance.goToEntertainmentProbabilityGlobal += value;
                break;

            case GeneralRuleType.GoToWorkProbability:
                StatsController.Instance.goToWorkProbabilityGlobal += value;
                break;

            case GeneralRuleType.InfectionProbability:
                StatsController.Instance.infectionProbability += value;
                break;

            case GeneralRuleType.SpreadVirusProbability:
                StatsController.Instance.spreadVirusProbabilityGlobal += value;
                break;

            case GeneralRuleType.WashHandsProbability:
                StatsController.Instance.handWashProbabilityPerHour += value;
                break;

            case GeneralRuleType.HospitalDeathRate:
                StatsController.Instance.hospitalDeathRate += value;
                break;

            case GeneralRuleType.HospitalCapacity:

                break;
        }
    }

    Rule[] SelectRuleSet()
    {
        List<Rule> chosenRules = new List<Rule>();

        List<Rule> rulePoolCopy = new List<Rule>(rulePool);

        List<Rule> validRules = rulePoolCopy.Where(x => x.cost < StatsController.Instance.money).ToList();

        if (validRules.Count == 0)
            validRules = allRules.Where(x => x.cost < StatsController.Instance.money).ToList();

        Rule selectedRule = validRules[Random.Range(0, validRules.Count)];
        chosenRules.Add(selectedRule);
        rulePoolCopy.Remove(selectedRule);

        for (int i = 0; i < ruleCountToChoseFrom - 1; i++)
        {
            if (rulePoolCopy.Count <= 0)
                chosenRules.Add(allRules[Random.Range(0, allRules.Count)]);
            else
            {
                int randomRuleIndex = Random.Range(0, rulePoolCopy.Count);
                chosenRules.Add(rulePoolCopy[randomRuleIndex]);
                rulePoolCopy.RemoveAt(randomRuleIndex);
            }
        }

        return chosenRules.ToArray();
    }

    private void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            timer += ruleInterval;
            UIController.Instance.ShowRuleScreen(SelectRuleSet());
        }
    }

    #endregion

    #region properties
    public static RuleManager Instance { get => _instance; }
    #endregion
}
