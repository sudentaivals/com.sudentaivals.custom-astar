using System.Collections.Generic;
using Priority_Queue;
using UnityEngine;

namespace sudentaivals.CustomAstar
{
    public class Astar
    {
        private FastPriorityQueue<Node> _openList;
        private HashSet<Node> _openSet;
        private readonly int CARDINAL_COST = 70;
        private readonly int DIAGONAL_COST = 99;
        private readonly int ADDITIONAL_COST = 35;
        private int _gCost;
        public Astar(int numOfNodes = 2000)
        {
            _openList = new(numOfNodes);
            _openSet = new();
        }
        private int Octile8DirCost(Node currentNode, Node goalNode)
        {
            var dX = Mathf.Abs(currentNode.X - goalNode.X);
            var dY = Mathf.Abs(currentNode.Y - goalNode.Y);
            int diffCost = DIAGONAL_COST - CARDINAL_COST;
            
            return dX > dY 
                ? CARDINAL_COST * dX + diffCost * dY
                : CARDINAL_COST * dY + diffCost * dX;
        }
        public Stack<Node> FindPath(Node startNode, Node goalNode)
        {
            var currentGrid = GridManager.Instance.CurrentGrid;
            currentGrid.Reset();
            _openList.Clear();

            _openList.Enqueue(startNode, Octile8DirCost(startNode, goalNode));
            startNode.IsInOpenList = true;
            Node node = null;

            while (_openList.Count > 0)
            {
                node = _openList.Dequeue();
                node.IsInOpenList = false;
                node.IsClosed = true;

                if (node.WorldPosition == goalNode.WorldPosition)
                {
                    return CalculatePath(node);
                }

                var neighbors = currentGrid.GetNeighbors(node);
                foreach (Node neighbourNode in neighbors)
                {
                    if (neighbourNode.IsClosed) continue;

                    if (neighbourNode.IsObstacle)
                    {
                        neighbourNode.IsClosed = true;
                        continue;
                    }

                    _gCost = node.G + Octile8DirCost(node, neighbourNode);
                    int addition = neighbourNode.IsNearObstacle ? ADDITIONAL_COST : 0;
                    int tentativeGCost = _gCost + addition;

                    if (!neighbourNode.IsInOpenList)
                    {
                        neighbourNode.G = tentativeGCost;
                        neighbourNode.H = Octile8DirCost(neighbourNode, goalNode);
                        neighbourNode.Parent = node;
                        neighbourNode.F = neighbourNode.G + neighbourNode.H;
                        _openList.Enqueue(neighbourNode, neighbourNode.F);
                        neighbourNode.IsInOpenList = true;
                    }
                    else if (tentativeGCost < neighbourNode.G)
                    {
                        neighbourNode.G = tentativeGCost;
                        neighbourNode.F = neighbourNode.G + neighbourNode.H;
                        neighbourNode.Parent = node;
                        _openList.UpdatePriority(neighbourNode, neighbourNode.F);
                    }
                }
            }

            return null;
        }
        public Stack<Node> FindPath(Vector3 startPos, Vector3 goalPos)
        {
            if(GridManager.Instance.CurrentGrid == null) return null;
            var startNode = GridManager.Instance.CurrentGrid.GetNodeByCoordinates(startPos);
            if(startNode == null) return null;
            var endNode = GridManager.Instance.CurrentGrid.GetNodeByCoordinates(goalPos);
            if(endNode == null || endNode.IsObstacle) return null;
            return FindPath(startNode, endNode);
        }
        private Stack<Node> CalculatePath(Node node)
        {
            Stack<Node> path = new();
            while(node != null)
            {
                path.Push(node);
                node = node.Parent;
            }
            return path;
            //return path.ToList();
        }
    }
}
