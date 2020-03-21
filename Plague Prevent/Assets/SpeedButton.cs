using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedButton : MonoBehaviour
{
    public void SetSpeed(int timescale)
    {
        Time.timeScale = timescale;
    }
}
