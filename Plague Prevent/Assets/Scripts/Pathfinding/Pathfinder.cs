using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Pathfinder 
{
    #region fields
    static List<Node> _nodes = new List<Node>();
    #endregion

    #region methods
    public static void RegisterNode(Node _node)
    {
        _nodes.Add(_node);
    }

    public static List<Node> FindPath(Node _startNode, Node _targetNode)
    {
		bool pathFound = false;
        List<Node> path = new List<Node>();

		List<Node> openSet = new List<Node>();
		HashSet<Node> closedSet = new HashSet<Node>();
		openSet.Add(_startNode);
		while (openSet.Count > 0 && !pathFound)
		{
			Node node = openSet[0];
			for (int i = 1; i < openSet.Count; i++)
			{
				if (openSet[i].FCost < node.FCost || openSet[i].FCost == node.FCost)
				{
					if (openSet[i].HCost < node.HCost)
						node = openSet[i];
				}
			}

			openSet.Remove(node);
			closedSet.Add(node);

			if (node == _targetNode)
			{
				path = RetracePath(_startNode, _targetNode);
				pathFound = true;
			}
			else
			{
					foreach (Node neighbour in node.Neighbours)
					{
						if (closedSet.Contains(neighbour))
						{
							continue;
						}

						int newCostToNeighbour = node.GCost + GetDistance(node, neighbour);
						if (newCostToNeighbour < neighbour.GCost || !openSet.Contains(neighbour))
						{
							neighbour.SetGCost(newCostToNeighbour);
							neighbour.SetHCost(GetDistance(neighbour, _targetNode));
							neighbour.SetParrent(node);

							if (!openSet.Contains(neighbour))
								openSet.Add(neighbour);
						}
					}
			}
		}
		return path;
    }

	static List<Node> RetracePath(Node startNode, Node endNode)
	{
		List<Node> path = new List<Node>();
		Node currentNode = endNode;

		while (currentNode != startNode)
		{
			path.Add(currentNode);
			currentNode = currentNode.CurrentParent;
		}
		path.Reverse();

		return path;
	}

	static int GetDistance(Node nodeA, Node nodeB)
	{
		int dstX = (int) Mathf.Abs(nodeA.transform.position.x - nodeB.transform.position.x);
		int dstY = (int)Mathf.Abs(nodeA.transform.position.z - nodeB.transform.position.z);

		if (dstX > dstY)
			return 14 * dstY + 10 * (dstX - dstY);
		return 14 * dstX + 10 * (dstY - dstX);
	}
	#endregion

	#region properties

	public static List<Node> Nodes { get => _nodes; set => _nodes = value; }
    #endregion
}
