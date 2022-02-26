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
    public bool canShoot = true, canDash = true;

    private Path path;
    private int currWaypoint;
    private bool reachedPathEnd = false;
    private Seeker seeker;
    private Rigidbody2D rb;
    [SerializeField] private float seekPlayerDist, attackPlayerDist, retreatDist;
    [SerializeField] private float maxShotTime, maxDashTime;
    private float currShotTime, currDashTime;
    [SerializeField] private float dashSpeed;
    private Animator anim;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        seeker = GetComponent<Seeker>();
        anim = GetComponentInChildren<Animator>();

        currShotTime = maxShotTime;
        currDashTime = maxDashTime;

        InvokeRepeating("UpdatePath", 0f, .5f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (gameObject.CompareTag("RangeEnemy"))
        {
            RangeEnemyMove();
        }
        else if (gameObject.CompareTag("MeleeEnemy"))
        {
            DashEnemyMove();
        }
    }

    void RangeEnemyMove()
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

    void DashEnemyMove()
    {
        if (Vector2.Distance(rb.position, target.position) < seekPlayerDist && Vector2.Distance(rb.position, target.position) > attackPlayerDist)
        {
            FollowPlayer(false);
        }
        else if (Vector2.Distance(rb.position, target.position) < attackPlayerDist)
        {
            AttackPlayer();
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
            if (canDash)
            {
                DashAttack();
            }
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
            anim.SetTrigger("Shoot");
            currShotTime = maxShotTime; 
        }
        else
        {
            currShotTime -= Time.deltaTime;
        }
    }

    private void DashAttack()
    {
        Vector3 dir = (target.position - transform.position).normalized;
        Vector2 dir2d = new Vector2(dir.x * 15, dir.y * 15);
        anim.SetTrigger("Dash");
        if (currDashTime <= 0)
        {
            StartCoroutine(Cooldown());
            currDashTime = maxDashTime;           
        }
        else
        {
            
            //rb.velocity = (new Vector2(dir.x * dashSpeed * Time.deltaTime, dir.y * dashSpeed * Time.deltaTime));
            //rb.MovePosition(rb.position + dir2d * dashSpeed * Time.fixedDeltaTime);
            transform.position += dir * dashSpeed * Time.fixedDeltaTime;
            //transform.position = Vector2.MoveTowards(transform.position, dir2d, dashSpeed * Time.fixedDeltaTime);
            //rb.AddForce(dir2d * dashSpeed * Time.deltaTime);

            currDashTime -= Time.deltaTime;
        }
    }

    IEnumerator Cooldown()
    {
        canDash = false;
        yield return new WaitForSeconds(3f);
        canDash = true; 
    }
}
