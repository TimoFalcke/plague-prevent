using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[SelectionBase]
public class Location : MonoBehaviour
{
    #region fields
    [SerializeField]
    LocationType type;
    [SerializeField]
    Node _node;


    List<Agent> _agents;
    Node[] _interiorNodes;
    [SerializeField]
    GameObject _interiorNodesContainer;
    [Header("Visual")]
    [SerializeField]
    GameObject _icon;
    [SerializeField]
    TextMeshPro _agentCounter;
    #endregion

    private void Awake()
    {
        _agents = new List<Agent>();
        _interiorNodes = _interiorNodesContainer.GetComponentsInChildren<Node>();

        switch (type)
        {
            case LocationType.HOSPITAL:
                LocationController.Instance.RegisterHospital(this);
                break;
            case LocationType.FACTORY:
            case LocationType.OFFICE:
                LocationController.Instance.RegisterWorkspace(this);
                break;

            case LocationType.SUPERMARKET:
            case LocationType.SHOPPING:
            case LocationType.CULTURE:
            case LocationType.GASTRONOMY:
            case LocationType.PARTY:
                LocationController.Instance.RegisterEntertainment(this);
                break;
        }
    }

    private void Update()
    {
        _agentCounter.text = _agents.Count.ToString();
    }

    public void RegisterAgent(Agent agent)
    {
        _agents.Add(agent);
    }

    public void Unregister(Agent agent)
    {
        _agents.Remove(agent);
    }

    #region properties
    public Node Node { get => _node; }
    public GameObject Icon { get => _icon;  }

    public LocationType LocationType { get => type; }


    #endregion
}
