using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int maxHealth;
    private Animator anim;
    public HealthBar healthBar;

    private int health;
    private bool justDamaged;
    private float cooldown = 1f;

    void Start()
    {  
        health = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        anim = GetComponentInChildren<Animator>();
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
            }
        }       
    }
}
