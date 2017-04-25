using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class routeForPathFollowing : MonoBehaviour
{

    public Transform[] nodes;

    public Vector3 retrievePosition(int id)
    {
        return nodes[id].position;
    }
}
