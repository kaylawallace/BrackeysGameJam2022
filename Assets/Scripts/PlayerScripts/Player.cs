using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int maxHealth;
    private Animator anim;
    public HealthBar healthBar;
    public LevelLoader levelLoader;

    private int health;
    private bool justDamaged;
    private float cooldown = 1f;

    void Start()
    {  
        health = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        anim = GetComponentInChildren<Animator>();
        levelLoader = GameObject.Find("LevelLoader").GetComponent<LevelLoader>();
        anim.SetBool("Dead", false);
    }

    private void Update()
    {
        if (justDamaged)
        {
            cooldown -= Time.deltaTime;

            if (cooldown <= 0)
            {
                justDamaged = false;
            }
        }
    }

    public void TakeDamage(int damage)
    {
        if (!justDamaged)
        {
            health -= damage;
            healthBar.SetHealth(health);
            anim.SetTrigger("Damage");

            if (health <= 0)
            {
                //death
                Die();
            }
        }       
    }

    void Die()
    {
        anim.SetBool("Dead", true);
        GetComponent<PlayerMovement>().canMove = false;
        GetComponent<PlayerMovement>().canDash = false;
        levelLoader.ReloadCurrentScene(1f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Hole"))
        {
            anim.SetTrigger("Fallen");
            levelLoader.ReloadCurrentScene(0f);
        }
    }
}
