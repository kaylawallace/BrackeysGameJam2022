using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;

    private Transform player;
    private Vector2 target;
    private Rigidbody2D rb;
    private Vector3 dir;
    [SerializeField] private int rangedDamage, meleeDamage;

    private void Start()
    {
        player = GameObject.Find("Player").transform;
        target = new Vector2(player.position.x, player.position.y);
        dir = ((Vector3)target - transform.position).normalized;
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Shoot();
    }

    void Shoot()
    {      
        transform.position += dir * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // do damage to player 
            collision.GetComponent<Player>().TakeDamage(rangedDamage);
            Destroy(gameObject);
        }
        else if (collision.CompareTag("Environment"))
        {
            Destroy(gameObject);
        }
    }

}
