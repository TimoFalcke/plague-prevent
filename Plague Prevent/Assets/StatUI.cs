using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatUI : MonoBehaviour
{
    [SerializeField] DisplayedStat stat;

    [SerializeField] TextMeshProUGUI text;

    // Update is called once per frame
    void Update()
    {
       switch (stat)
        {
            case DisplayedStat.Healthy:
                text.text = (StatsController.Instance.population - StatsController.Instance.infectedCount - StatsController.Instance.deathCount).ToString();
                break;

            case DisplayedStat.Infected:
                text.text = StatsController.Instance.infectedCount.ToString();
                break;

            case DisplayedStat.Dead:
                text.text = StatsController.Instance.deathCount.ToString();
                break;

            case DisplayedStat.Acceptance:
                text.text = StatsController.Instance.approval.ToString();
                break;

            case DisplayedStat.Money:
                text.text = StatsController.Instance.currency.ToString();
                break;

            case DisplayedStat.DateTime:
                text.text = StatsController.Instance.currentDateTime.ToString("dd.MM.yyyy - HH:mm");
                break;
        }
    }
}

enum DisplayedStat
{
    Healthy,
    Infected,
    Dead,
    Acceptance,
    DateTime,
    Money,
    Income
}
