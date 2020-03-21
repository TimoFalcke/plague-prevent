using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class Location : MonoBehaviour
{
    #region fields
    [SerializeField]
    LocationType type;
    [SerializeField]
    Node _node;

    Node[] _interiorNodes;
    [SerializeField]
    GameObject _interiorNodesContainer;
    #endregion

    private void Awake()
    {
        _interiorNodes = _interiorNodesContainer.GetComponentsInChildren<Node>();

        switch (type)
        {
            case LocationType.HOSPITAL:
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

    #region properties
    public Node Node { get => _node; }

  
    #endregion
}
