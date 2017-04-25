using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using System;

public class RouteSearcher : MonoBehaviour
{

    RouteManager routeRequest;

    Grid grid;
    private void Awake()
    {
        routeRequest = GetComponent<RouteManager>();
        grid = GetComponent<Grid>();
    }
    int GetDistance(Node nodeA, Node nodeB)
    {
        int distanceX = Mathf.Abs(nodeA.axisX - nodeB.axisX);
        int distanceY = Mathf.Abs(nodeA.axisY - nodeB.axisY);

        if (distanceX > distanceY)
            return 14 * distanceY + 10 * (distanceX - distanceY);
        return 14 * distanceX + 10 * (distanceY - distanceX);
    }
    Vector3[] Retrace(Node startNode, Node endNode)
    {
        List<Node> route = new List<Node>();
        Node currentNode = endNode;

        while (currentNode != startNode)
        {
            route.Add(currentNode);
            currentNode = currentNode.master;
        }
        Vector3[] checkpoints = SimplifyRoute(route);
        Array.Reverse(checkpoints);
        return checkpoints;


    }
    public void startFind(Vector3 startingSpot, Vector3 goalSpot)
    {
        StartCoroutine(FindRoute(startingSpot, goalSpot));
    }
    Vector3[] SimplifyRoute(List<Node> route)
    {
        List<Vector3> checkpoints = new List<Vector3>();
        Vector2 oldDirection = Vector2.zero;

        for (int i = 1; i < route.Count; i++)
        {
            Vector2 newDirection = new Vector2(route[i - 1].axisX - route[i].axisX, route[i - 1].axisY - route[i].axisY);
            if (newDirection != oldDirection)
            {
                checkpoints.Add(route[i].mazePosition);
            }
            oldDirection = newDirection;
        }
        return checkpoints.ToArray();
    }

    IEnumerator FindRoute(Vector3 startingSpot, Vector3 goalSpot)
    {


        Vector3[] checkpoints = new Vector3[0];
        bool Success = false;

        Node startNode = grid.mazeNode(startingSpot);
        Node goalNode = grid.mazeNode(goalSpot);
        if (startNode.move && goalNode.move)
        {


            Heap<Node> openSet = new Heap<Node>(grid.Max);
            HashSet<Node> closedSet = new HashSet<Node>();
            openSet.Add(startNode);

            while (openSet.Count > 0)
            {
                Node currentNode = openSet.Remove();
                closedSet.Add(currentNode);
                if (currentNode == goalNode)
                {
                    Success = true;
                    break;
                }
                foreach (Node near in grid.getSurroundingNodes(currentNode))
                {
                    if (!near.move || closedSet.Contains(near))
                    {
                        continue;
                    }
                    int newMovementCost = currentNode.gValue + GetDistance(currentNode, near);
                    if (newMovementCost < near.gValue || !openSet.Contains(near))
                    {
                        near.gValue = newMovementCost;
                        near.hValue = GetDistance(near, goalNode);
                        near.master = currentNode;

                        if (!openSet.Contains(near))
                            openSet.Add(near);

                    }
                }
            }
        }
        yield return null;
        if (Success)
        {
            checkpoints = Retrace(startNode, goalNode);
        }
        routeRequest.routeFinished(checkpoints, Success);
    }

   

    

  
}
