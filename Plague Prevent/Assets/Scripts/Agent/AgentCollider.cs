using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentCollider : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Agent thisAgent = gameObject.GetComponentInParent<Agent>();
        InfectionStateMachine otherAgent = other.gameObject.GetComponentInParent<InfectionStateMachine>();
        if (otherAgent && thisAgent.Status == InfectionStatus.CARRIER)
        {
            otherAgent.OnContact(thisAgent.Infectivity);
        }
    }
}
