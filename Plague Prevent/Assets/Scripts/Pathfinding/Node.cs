using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    #region fields
    Transform _transform;
    CircleCollider2D _collider;
    List<Node> _neighbours;
    int _gCost;
    int _hCost;
    Node _currentParent;
    [SerializeField]
    NodeType _nodeType;
    #endregion

    #region methods
    private void Awake()
    {
        _neighbours = new List<Node>();
        _transform = this.transform;
        Pathfinder.RegisterNode(this);
    }

    public void SetGCost(int value)
    {
        _gCost = value;
    }

    public void SetHCost(int value)
    {
        _hCost = value;
    }
    public void SetParrent(Node node)
    {
        _currentParent = node;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_nodeType == NodeType.STREET)
        {
            if (collision.gameObject.tag == "Node")
            {
                Node otherNode = collision.gameObject.GetComponent<Node>();
                if (otherNode._nodeType != NodeType.INTERIOR)
                {
                    _neighbours.Add(otherNode);
                    if (!otherNode.Neighbours.Contains(this))
                    {
                        otherNode.Neighbours.Add(this);
                        Debug.Log("Neighbour added");
                    }
                }
            }
        }
    }
    #endregion

    #region properties
    public Transform Position { get => _transform; }
    public Transform Transform { get => _transform; }
    public List<Node> Neighbours { get => _neighbours;}
    public int GCost { get => _gCost;  }
    public int HCost { get => _hCost;  }
    public int FCost { get => _gCost + _hCost; }
    public Node CurrentParent { get => _currentParent; }
    #endregion
}
