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
    public NodeType _nodeType;
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
        if (collision.gameObject.tag == "Node")
        {
            Node otherNode = collision.gameObject.GetComponent<Node>();

            switch (_nodeType)
            {
                case NodeType.STREET:

                    switch (otherNode.Type)
                    {
                        case NodeType.LOCATION:
                        case NodeType.STREET:
                        _neighbours.Add(otherNode);
                         if (!otherNode.Neighbours.Contains(this))
                         {
                                otherNode.Neighbours.Add(this);
                         }
                       break;
                    }
                    break;
                case NodeType.LOCATION:
                    switch (otherNode.Type)
                    {
                        case NodeType.STREET:
                            _neighbours.Add(otherNode);
                            if (!otherNode.Neighbours.Contains(this))
                            {
                                otherNode.Neighbours.Add(this);
                            }
                            RotateParentToStreet(otherNode);
                            
                            break;
                        case NodeType.INTERIOR:
                            _neighbours.Add(otherNode);
                            if (!otherNode.Neighbours.Contains(this))
                            {
                                otherNode.Neighbours.Add(this);
                            }
                            break;
                    }
                    break;
                case NodeType.INTERIOR:
                    switch (otherNode.Type)
                    {
                        case NodeType.LOCATION:
                        case NodeType.INTERIOR:
                            _neighbours.Add(otherNode);
                            if (!otherNode.Neighbours.Contains(this))
                            {
                                otherNode.Neighbours.Add(this);
                            }
                            break;
                    }
                    break;
            }

        }
    }

    private void RotateParentToStreet(Node node)
    {
        var dir = node.transform.position - transform.position;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;
        transform.parent.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        this.transform.parent.GetComponent<Location>().Icon.transform.localRotation = Quaternion.AngleAxis(-angle, Vector3.forward);
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
    public NodeType Type { get => _nodeType;  }
    #endregion
}
