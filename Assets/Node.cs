using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : IHeaps<Node>
{

    public bool move;
    public Vector3 mazePosition;
    public int axisX;
    public int axisY;

    public int gValue;
    public int hValue;
    public Node master;
    int heap;
    public int fValue
    {
        get
        {
            return gValue + hValue;
        }
    }
    public int CompareTo(Node inspectNode)
    {
        int contrast = fValue.CompareTo(inspectNode.fValue);
        if (contrast == 0)
        {
            contrast = hValue.CompareTo(inspectNode.hValue);
        }
        return -contrast;
    }
    public Node(bool _move, Vector3 _mazePos, int _axisX, int _axisY)
    {
        move = _move;
        mazePosition = _mazePos;
        axisX = _axisX;
        axisY = _axisY;
    }
    public int Heaps
    {
        get
        {
            return heap;
        }
        set
        {
            heap = value;
        }
    }

   
    
}