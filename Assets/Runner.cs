using System.Collections;
using UnityEngine;

public class Runner : MonoBehaviour
{

    public Transform goal;
    float speed = 40;
    Vector3[] route;
    int Goal;

    private void Start()
    {
        RouteManager.requestRoute(transform.position, goal.position, routeFound);
    }
   
    public void OnDrawGizmos()
    {
        if (route != null)
        {
            for (int i = Goal; i < route.Length; i++)
            {
                Gizmos.color = Color.black;
                Gizmos.DrawCube(route[i], Vector3.one);

                if (i == Goal)
                {
                    Gizmos.DrawLine(transform.position, route[i]);
                }
                else
                {
                    Gizmos.DrawLine(route[i - 1], route[i]);
                }
            }
        }
    }
    IEnumerator FollowRoute()
    {
        Vector3 currentCheckpoint = route[0];

        while (true)
        {
            if (transform.position == currentCheckpoint)
            {
                Goal++;
                if (Goal >= route.Length)
                {
                    Goal = 0;
                    route = new Vector3[0];
                    yield break;
                }
                currentCheckpoint = route[Goal];
            }
            transform.position = Vector3.MoveTowards(transform.position, currentCheckpoint, speed * Time.deltaTime);
            yield return null;
        }
    }
    public void routeFound(Vector3[] newRoute, bool routeSuccessful)
    {
        if (routeSuccessful)
        {
            route = newRoute;
            Goal = 0;
            StopCoroutine("FollowRoute");
            StartCoroutine("FollowRoute");
        }
    }

}
