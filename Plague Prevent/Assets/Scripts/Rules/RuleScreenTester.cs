using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuleScreenTester : MonoBehaviour
{
    [SerializeField] Rule[] rules;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            UIController.Instance.ShowRuleScreen(rules);
        }
    }
}
