using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentMovement : MonoBehaviour

{
    bool _targetReached = true;
    int _pathIndex;
    Vector3 _movingDirection;
    List<Node> _path;
    Node _currentNode;
    Node _targetNode;

    public void Initilize(Node startNode)
    {
        _currentNode = startNode;
    }

    public void Update()
    {
        if (_targetReached == false)
        {
            MoveAlongPath();
        }
    }

    public void SetPath(Node targetNode)
    {
        _path = Pathfinder.FindPath(_currentNode, targetNode);
        if (_path.Count >= 1)
        {
            _pathIndex = 0;
            _targetNode = _path[_pathIndex];
            _targetReached = false;
        }
    }

    public void MoveAlongPath()
    {
            GetTargetNode();
            GetMovingDirection();
            Move();
            CheckIfTargetNodeIsReached();
    }


    public void CheckIfTargetNodeIsReached()
    {
        if ((_pathIndex + 1) >= _path.Count == true)
        {
            if (Vector3.Distance(this.transform.position, _targetNode.Transform.position) < 0.1f)
            {
                _targetReached = true;
                _currentNode = _targetNode;
            }
        }
    }

    public void GetTargetNode()
    {
        if (Vector3.Distance(this.transform.position, _targetNode.transform.position) <= 0.1f)
        {
            _pathIndex += 1;
            _targetNode = _path[_pathIndex];
        }
    }


    public void GetMovingDirection()
    {
        _movingDirection = (_targetNode.transform.position - this.transform.position).normalized;
    }

    // Speed noch an Agent Speed anpassen
    public void Move()
    {
        this.transform.position += _movingDirection * 20 * Time.deltaTime;
    }


}
