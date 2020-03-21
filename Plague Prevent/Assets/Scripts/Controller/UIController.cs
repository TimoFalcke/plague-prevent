using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : Controller
{
    #region fields
    private static UIController _instance;
    [SerializeField] DecisionScreen decisionScreen;

    #endregion

    #region initilization
    private void Awake()
    {
        GetInstance();
    }
    #endregion

    #region methods
    public UIController GetInstance()
    {
        if (_instance == null)
        {
            _instance = this;
        }

        return _instance;
    }

    public void ShowRuleScreen(Rule[] rules)
    {
        decisionScreen.Display(rules);
    }
    public void HideRuleScreen()
    {
        decisionScreen.Hide();
    }

    #endregion

    #region properties
    public static UIController Instance { get => _instance; }
    #endregion
}
