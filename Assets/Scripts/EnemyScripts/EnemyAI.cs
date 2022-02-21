using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{
    public Transform target;
    public float speed = 200f;
    public float nextWaypointDist = 3f;

    private Path path;
    int currWaypoint;
    bool reachedPathEnd = false;

    private


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
