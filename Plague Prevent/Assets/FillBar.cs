using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FillBar : MonoBehaviour
{
    [SerializeField] DisplayedStat stat;

    [SerializeField] Image fillImage;
    void Update()
    {
        switch (stat)
        {
            case DisplayedStat.Infected:
                

                if (StatsController.Instance.Population == 0)
                    fillImage.fillAmount = 0;
                else
                    fillImage.fillAmount = 
                        (float)StatsController.Instance.Infected / (StatsController.Instance.Population - StatsController.Instance.Death);
                break;

            case DisplayedStat.Acceptance:
                fillImage.fillAmount =
                    Mathf.Clamp01(StatsController.Instance.approval);
                break;
        }
    }
}
