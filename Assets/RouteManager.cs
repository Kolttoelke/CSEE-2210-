using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RouteManager : MonoBehaviour
{
    Queue<RouteReqeust> routeRequest = new Queue<RouteReqeust>();
    RouteReqeust currentRouteRequest;

    static RouteManager now;
    RouteSearcher route;

    bool routeProcessing;

    void Awake()
    {
        now = this;
        route = GetComponent<RouteSearcher>();
    }
    public void routeFinished(Vector3[] route, bool success)
    {
        currentRouteRequest.recall(route, success);
        routeProcessing = false;
        TryProcessNext();
    }

    void TryProcessNext()
    {
        if (!routeProcessing && routeRequest.Count > 0)
        {
            currentRouteRequest = routeRequest.Dequeue();
            routeProcessing = true;
            route.startFind(currentRouteRequest.routeStart, currentRouteRequest.routeEnd);
        }
    }
   
    struct RouteReqeust
    {
        public Vector3 routeStart;
        public Vector3 routeEnd;
        public Action<Vector3[], bool> recall;
        public RouteReqeust(Vector3 _start, Vector3 _end, Action<Vector3[], bool> _recall)
        {
            routeStart = _start;
            routeEnd = _end;
            recall = _recall;
        }
    }
    public static void requestRoute(Vector3 routeStart, Vector3 routeEnd, Action<Vector3[], bool> recall)
    {
        RouteReqeust newRequest = new RouteReqeust(routeStart, routeEnd, recall);
        now.routeRequest.Enqueue(newRequest);
        now.TryProcessNext();
    }
}

