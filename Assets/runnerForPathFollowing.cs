using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class runnerForPathFollowing : MonoBehaviour
{

    public routeForPathFollowing route;
    public float Distance = 1f;

    public float speed = 10;
    public float rotationalSpeed = 50f;
    private int currentNode = 0;

   
    void changeNode()
    {
        currentNode++;
        if (currentNode >= route.nodes.Length)
        {
            currentNode = 0;
        }
    }
    void Update()
    {
        Vector3 destination = route.retrievePosition(currentNode);
        Vector3 offset = destination - transform.position;
        if (offset.sqrMagnitude > Distance)
        {
            offset = offset.normalized;
            transform.Translate(offset * speed * Time.deltaTime, Space.World);

            Quaternion lookRot = Quaternion.LookRotation(offset);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRot, rotationalSpeed * Time.deltaTime);
        }
        else
        {
            changeNode();
        }
    }

  


}
