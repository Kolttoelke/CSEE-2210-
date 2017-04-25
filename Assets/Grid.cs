using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public LayerMask obstacles;
    public Vector2 maze;
    public float Radius;
    Node[,] axis;

    float diameter;
    int axisX, axisY;

    void Awake()
    {
        diameter = Radius * 2;
        axisY = Mathf.RoundToInt(maze.y / diameter);
        axisX = Mathf.RoundToInt(maze.x / diameter);
        makeGrid();
    }
    

    public List<Node> getSurroundingNodes(Node node)
    {
        List<Node> surroundingNodes = new List<Node>();
        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (y == 0 && x == 0)
                    continue;

                int checkingX = node.axisX + x;
                int checkingY = node.axisY + y;

                if (checkingX >= 0 && checkingX < axisX && checkingY >= 0 && checkingY < axisY)
                {
                    surroundingNodes.Add(axis[checkingX, checkingY]);
                }
            }
        }
        return surroundingNodes;
    }
    public int Max
    {
        get
        {
            return axisX * axisY;
        }
    }
   

    void makeGrid()
    {
        axis = new Node[axisX, axisY];
        Vector3 bottomLeft = transform.position - Vector3.right * maze.x / 2 - Vector3.forward * maze.y / 2;
        for (int x = 0; x < axisX; x++)
        {
            {
                for (int y = 0; y < axisY; y++)
                {
                    Vector3 mazePoint = bottomLeft + Vector3.right * (x * diameter + Radius) + Vector3.forward * (y * diameter + Radius);
                    bool accessible = !(Physics.CheckSphere(mazePoint, Radius, obstacles));
                    axis[x, y] = new Node(accessible, mazePoint, x, y);
                }
            }
        }
    }
    public Node mazeNode(Vector3 mazePosition)
    {
        float percentageX = (mazePosition.x + maze.x / 2) / maze.x;
        float percentageY = (mazePosition.z + maze.y / 2) / maze.y;
        percentageX = Mathf.Clamp01(percentageX);
        percentageY = Mathf.Clamp01(percentageY);

        int x = Mathf.RoundToInt((axisX - 1) * percentageX);
        int y = Mathf.RoundToInt((axisY - 1) * percentageY);
        return axis[x, y];
    }


}