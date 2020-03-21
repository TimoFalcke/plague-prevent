using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestFinder : MonoBehaviour
{
    [SerializeField]
    private Node _startNode;

    [SerializeField]
    private Node _endNode;

    AgentMovement _agentMovement;

    private void Awake()
    {
        _agentMovement = GetComponent<AgentMovement>();
        _agentMovement.SetPath(_endNode);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
