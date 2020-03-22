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
        string statText = "";
        switch (stat)
        {
            case DisplayedStat.Immun:
                statText = (StatsController.Instance.Immuns).ToString("000");
                text.text = AddTransparency(statText);
                break;

            case DisplayedStat.Healthy:
                statText = (StatsController.Instance.Healthy).ToString("000");
                text.text = AddTransparency(statText);
                break;

            case DisplayedStat.Carrier:
                statText = StatsController.Instance.Carrier.ToString("000");
                text.text = AddTransparency(statText);
                break;

            case DisplayedStat.Infected:
                statText = StatsController.Instance.Infected.ToString("000");
                text.text = AddTransparency(statText);
                break;

            case DisplayedStat.Dead:
                statText = StatsController.Instance.Death.ToString("000");
                text.text = AddTransparency(statText);
                break;

            case DisplayedStat.Acceptance:
                statText = (StatsController.Instance.approval * 100).ToString("000") + "%";
                text.text = AddTransparency(statText);// (statText.Substring(0, 2)) + "<alpha=#FF>" + AddTransparency(statText.Substring(2, 2));
                break;

            case DisplayedStat.Money:
                statText = StatsController.Instance.money.ToString("0000");
                text.text = AddTransparency(statText);
                break;

            case DisplayedStat.DateTime:
                statText = StatsController.Instance.currentDateTime.ToString("dd.MM.yyyy  HH:mm");
                //statText += AddTransparency(StatsController.Instance.currentDateTime.ToString("dd."));
                //statText += AddTransparency(StatsController.Instance.currentDateTime.ToString("MM.yyyy  "));
                //statText += AddTransparency(StatsController.Instance.currentDateTime.ToString("HH:mm"));
                text.text = statText;
                break;

            case DisplayedStat.Income:
                statText = StatsController.Instance.income.ToString("000");
                text.text = "+" + AddTransparency(statText);
                break;

            case DisplayedStat.Day:
                statText = (Mathf.FloorToInt((StatsController.Instance.WorldTime + StatsController.Instance.startHour) / 24f) + 1).ToString("000");
                text.text = AddTransparency(statText);
                break;

            case DisplayedStat.VictoryText:
                statText = $"Es hat <color=#EE1E55>{ (Mathf.FloorToInt((StatsController.Instance.WorldTime + StatsController.Instance.startHour) / 24f) + 1) } <color=#FFFF>Tage gedauert und <color=#EE1E55>{ StatsController.Instance.Death } <color=#FFFF>Opfer gefordert";
                text.text = statText;
                break;
        }
    }

    string AddTransparency(string original)
    {
        string returnString = original;

        if (original[0] == '0')
        {
            int split = 0;
            for (split = 0; split < original.Length; split++)
            {
                if (original[split] != '0' && original[split] != '.')
                    break;
            }

            returnString = "<alpha=#33>" + original.Substring(0, split);
            returnString += "<alpha=#FF>" + original.Substring(split, original.Length - split);
        }

        return returnString;
    }
}

public enum DisplayedStat
{
    Immun,
    Healthy,
    Carrier,
    Infected,
    Dead,
    Acceptance,
    DateTime,
    Money,
    Income,
    Day,
    VictoryText
}
