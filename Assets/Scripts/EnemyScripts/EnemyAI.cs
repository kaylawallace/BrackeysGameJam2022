using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{
    public Transform target;
    public float speed = 5f;
    public float nextWaypointDist = 0.5f;
    public GameObject projectile;
    public bool canShoot = true;

    private Path path;
    private int currWaypoint;
    private bool reachedPathEnd = false;
    private Seeker seeker;
    private Rigidbody2D rb;
    [SerializeField] private float seekPlayerDist, attackPlayerDist, retreatDist;
    [SerializeField] private float maxShotTime;
    private float currShotTime;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        seeker = GetComponent<Seeker>();

        currShotTime = maxShotTime;

        InvokeRepeating("UpdatePath", 0f, .5f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Vector2.Distance(rb.position, target.position) < seekPlayerDist && Vector2.Distance(rb.position, target.position) > attackPlayerDist)
        {
            FollowPlayer(false);
        }
        else if (Vector2.Distance(rb.position, target.position) < attackPlayerDist && Vector2.Distance(rb.position, target.position) > retreatDist)
        {
            AttackPlayer();
        }
        else if (Vector2.Distance(rb.position, target.position) < retreatDist)
        {
            FollowPlayer(true);
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

    // reverse used for retreat 
    private void FollowPlayer(bool reverse)
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

        if (reverse)
        {
            dir = -dir; 

        }

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

    private void AttackPlayer()
    {
        if (gameObject.CompareTag("MeleeEnemy"))
        {
            // implement melee attacks here
        }
        else if (gameObject.CompareTag("RangeEnemy"))
        {
            // implement ranged attacks here 
            if (canShoot)
            {
                RangedAttack();
            }            
        }
    }

    private void RangedAttack()
    {
        if (currShotTime <= 0)
        {
            Instantiate(projectile, transform.position, Quaternion.identity);
            currShotTime = maxShotTime; 
        }
        else
        {
            currShotTime -= Time.deltaTime;
        }
    }
}
