using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Appartment : Location
{
    [SerializeField]
    int _agentCount;

    public void Start()
    {
        SpawnAgents();
    }

    public void SpawnAgents()
    {
        for (int i = 0; i < _agentCount; i++)
        {
            Agent agent = GameObject.Instantiate(AgentController.Instance.AgentPrefab, this.transform.position, Quaternion.identity, AgentController.Instance.AgentContainer.transform).GetComponent<Agent>();
            agent.Initilize(this);
        }
    }
}
