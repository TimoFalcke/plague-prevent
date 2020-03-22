using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentMovement : MonoBehaviour

{
    [SerializeField] float nodeMaxOffset;

    Agent _agent;
    int _pathIndex;
    Vector3 _movingDirection;
    List<Node> _path;
    Node _currentNode;
    Node _nextNode;
    float _movementSpeed ;
    Vector3 _movementModifier;
    float _movementModStrength = 1f;
    float movementModTimer = 0;
    float movementModInterval = 600;

    bool _movingRandom = false;
    bool _targetReached = true;

    Vector3 nextNodeOffsetPos;
    List<Node> _tempInteriorNodes;
    float _interiorTimer;
    float _interiorMinMovementInterval = 5;
    float _interiorMaxMovementInterval = 15;

    private void Awake()
    {
        _agent = this.transform.parent.GetComponent<Agent>();   
    }

    public void Initilize(Node startNode)
    {
        _currentNode = startNode;
        _movementSpeed = Random.Range(AgentController.Instance.MinSpeed, AgentController.Instance.MaxSpeed);
        _tempInteriorNodes = new List<Node>();
    }

    public void Update()
    {
        if (_targetReached == false)
        {
            MoveAlongPath();
        }

       // if(_movingRandom == true)
       // {
       //     GetNextInteriorNode();
       //     GetInteriorMovingDirection();
       //     Move(); 
       //
       // }
    }

    public void SetPath(Node targetNode)
    {
        _path = Pathfinder.FindPath(_currentNode, targetNode);
        if (_path.Count > 1)
        {
            //_path = path;
            _pathIndex = 0;
            _nextNode = _path[_pathIndex];
            _targetReached = false;
            _movingRandom = false;
            nextNodeOffsetPos = _nextNode.transform.position;
            _agent.UnsetLocation();
        }
    }

    public void MoveAlongPath()
    {
            GetNextNode();
            //ModifyMovementDirection();
            GetMovingDirection();
            Move();
            CheckIfTargetNodeIsReached();
    }



    public void CheckIfTargetNodeIsReached()
    {
        if ((_pathIndex + 1) >= _path.Count == true)
        {
            if (Vector3.Distance(this.transform.position, _nextNode.Transform.position) < 0.1f)
            {
                _targetReached = true;
                _agent.SetCurrentLocation(_path[_path.Count-1].transform.parent.GetComponent<Location>());
                //_movingRandom = true;
                //_tempInteriorNodes.Clear();
                //_currentNode = _nextNode;
                //GetInteriorNodes();
                //_path = Pathfinder.FindPath(_currentNode, GetRandomInteriorNode());
            }
        }
    }

    public void CheckIfInteriorTargetNodeIsReached()
    {
            if (Vector3.Distance(this.transform.position, _nextNode.Transform.position) < 0.1f)
            {
                _currentNode = _nextNode;
                _path = Pathfinder.FindPath(_currentNode, GetRandomInteriorNode());

            }
    }

    public void GetNextNode()
    {
        if (Vector3.Distance(this.transform.position, nextNodeOffsetPos/*_nextNode.transform.position*/) <= 0.1f)
        {
            _pathIndex += 1;
            _currentNode = _nextNode;
            _nextNode = _path[_pathIndex];

            nextNodeOffsetPos = _nextNode.transform.position +
                (Vector3)Random.insideUnitCircle * ((_nextNode._nodeType == NodeType.STREET) ? nodeMaxOffset : 0);
        }
    }

    void GetInteriorNodes()
    {
        for (int i = 0; i < _currentNode.Neighbours.Count; i++)
        {
            if(_currentNode.Neighbours[i].Type == NodeType.INTERIOR)
            {
                _tempInteriorNodes.Add(_currentNode.Neighbours[i]);
            }
        }
    }

    Node GetRandomInteriorNode()
    {
        return _tempInteriorNodes[Random.Range(0, _tempInteriorNodes.Count)];
    }

    public void GetNextInteriorNode()
    {
        if (Vector3.Distance(this.transform.position, nextNodeOffsetPos/*_nextNode.transform.position*/) <= 0.1f)
        {
            _pathIndex += 1;
            _currentNode = _nextNode;
            _nextNode = GetRandomInteriorNode();
        }
    }

    public void GetInteriorMovingDirection()
    {
        _movingDirection = (_nextNode.transform.position - this.transform.position).normalized;
    }


    public void GetMovingDirection()
    {
        _movingDirection = (nextNodeOffsetPos/*_nextNode.transform.position*/ - this.transform.position).normalized;
    }

    public void Move()
    {
        this.transform.parent.position += (_movingDirection + _movementModifier).normalized * _movementSpeed * Time.deltaTime;
    }

    bool vertical = true;


    private void ModifyMovementDirection()
    {
        if (movementModTimer < Time.time)
        {
            if (_movingDirection.x < 0.1)
            {
                _movementModifier = new Vector3(Random.Range(-_movementModStrength, _movementModStrength), 0, 0);

            }
            if(_movingDirection.y < 0.1)
            {
                _movementModifier = new Vector3(0, Random.Range(-_movementModStrength, _movementModStrength), 0);

            }
            movementModTimer = Time.time + movementModInterval * Time.deltaTime;
        }
    }



    public Agent Agent { get => _agent; }
}
