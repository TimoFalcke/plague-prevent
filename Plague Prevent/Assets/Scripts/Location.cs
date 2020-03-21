using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class Location : MonoBehaviour
{
    #region fields
    [SerializeField]
    Node _node;

    [SerializeField]
    Node[] interiorNodes;
    #endregion

    #region properties
    public Node Node { get => _node; }
    #endregion
}
