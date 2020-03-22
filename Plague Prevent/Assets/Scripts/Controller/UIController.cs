using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : Controller
{
    #region fields
    private static UIController _instance;
    [SerializeField] DecisionScreen decisionScreen;
    [SerializeField] GameObject entryScreen;
    [SerializeField] GameObject victoryScreen;
    [SerializeField] GameObject gameOverScreen;
    [SerializeField] float entryScreenDelay = 10f;

    #endregion

    #region initilization
    private void Awake()
    {
        GetInstance();
    }

    private void Start()
    {
        Invoke("ShowEntryScreen", entryScreenDelay);
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
        MusicController.Instance.SwtitchToLayer2();
    }
    public void HideRuleScreen()
    {
        decisionScreen.Hide();
        MusicController.Instance.SwtitchToLayer1();
    }

    public void ShowVictoryScreen()
    {
        victoryScreen.SetActive(true);
    }

    public void ShowGameOverScreen()
    {
        gameOverScreen.SetActive(true);
    }

    public void ShowEntryScreen()
    {
        AgentController.Instance.InfectRandom();
        entryScreen.SetActive(true);
    }

    #endregion

    #region properties
    public static UIController Instance { get => _instance; }
    #endregion
}
