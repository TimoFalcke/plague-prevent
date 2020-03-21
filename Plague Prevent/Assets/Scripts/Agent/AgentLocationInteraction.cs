using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AgentSchedule))]
public class AgentLocationInteraction : MonoBehaviour
{
    bool insideLocation;

    Location wantedLocation;

    private void Start()
    {
        GetComponent<AgentSchedule>().OnNewTargetChosen += UpdateWantedLocation;
    }

    void UpdateWantedLocation(Location newLocation)
    {
        wantedLocation = newLocation;
        insideLocation = false;
    }

    // TODO: remove later
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("Location"))
    //    {
    //        Location touchedLocation = collision.GetComponent<Location>();

    //        if (touchedLocation == wantedLocation)
    //        {
    //            EnterLocation(wantedLocation);
    //        }
    //    }
    //}

    void EnterLocation(Location location)
    {
        insideLocation = true;
    }
}
