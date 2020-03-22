using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentCollider : MonoBehaviour
{
    private bool isInBuilidng = false;
    private void OnTriggerEnter2D(Collider2D other)
    {
        Agent thisAgent = gameObject.GetComponentInParent<Agent>();
        InfectionStateMachine otherAgent = other.gameObject.GetComponentInParent<InfectionStateMachine>();
        InfectionStatus[] transmitter = new[] {InfectionStatus.CARRIER, InfectionStatus.SICK, InfectionStatus.SEVERE};
        if (otherAgent && Array.Exists(transmitter, ele => ele == thisAgent.Status))
        {
            otherAgent.OnContact(thisAgent.Infectivity);
        }

        if (other.gameObject.CompareTag("Node"))
        {
            Node hitNode = other.gameObject.GetComponent<Node>();
            if (hitNode.Type == NodeType.INTERIOR && hitNode.GetComponentInParent<Location>().LocationType == LocationType.RESIDENTAL)
            {
                isInBuilidng = true;
                StartCoroutine(nameof(WashHands));
            } else if (hitNode.Type == NodeType.INTERIOR && hitNode.GetComponentInParent<Location>().LocationType == LocationType.HOSPITAL)
            {
                thisAgent.InfectionStateMachine.DeathRate = StatsController.Instance.hospitalDeathRate;
            }
        }
        
    }


    private IEnumerator WashHands()
    {
        Agent agent = gameObject.GetComponentInParent<Agent>();
        while (isInBuilidng)
        {
            agent.InfectionStateMachine.RemoveCarrier();
            yield return new WaitForSeconds(1);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Node"))
        {
            Agent agent = gameObject.GetComponentInParent<Agent>();
            Node hitNode = other.gameObject.GetComponent<Node>();
            if (hitNode.Type == NodeType.INTERIOR && hitNode.GetComponentInParent<Location>().LocationType == LocationType.RESIDENTAL)
            {
                isInBuilidng = false;
            } else if (hitNode.Type == NodeType.INTERIOR && hitNode.GetComponentInParent<Location>().LocationType == LocationType.HOSPITAL)
            {
                agent.InfectionStateMachine.DeathRate =
                    StatsController.Instance.infectedDeathRate;
            }
        }
    }
}
