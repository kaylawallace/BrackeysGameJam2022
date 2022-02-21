using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{
    public Transform target;
    public float speed = 5f;
    public float nextWaypointDist = 0.5f;

    private Path path;
    private int currWaypoint;
    private bool reachedPathEnd = false;
    private Seeker seeker;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        seeker = GetComponent<Seeker>();
        InvokeRepeating("UpdatePath", 0f, .5f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Vector2.Distance(rb.position, target.position) < 3f)
        {
            FollowPlayer();   
        }       
    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currWaypoint = 0;
        }
    }

    private void FollowPlayer()
    {
        if (path == null)
        {
            return;
        }

        if (currWaypoint >= path.vectorPath.Count)
        {
            reachedPathEnd = true;
            return;
        }
        else
        {
            reachedPathEnd = false;
        }

        Vector2 dir = ((Vector2) path.vectorPath[currWaypoint] - rb.position).normalized;
        Vector2 force = dir * speed * Time.deltaTime;
        rb.MovePosition(rb.position + force * speed * Time.deltaTime);

        float dist = Vector2.Distance(rb.position, path.vectorPath[currWaypoint]);
        if (dist < nextWaypointDist)
        {
            currWaypoint++;
        }
    }

    private void UpdatePath()
    {
        if (seeker.IsDone())
        {
            seeker.StartPath(rb.position, target.position, OnPathComplete);
        }   
    }
}
