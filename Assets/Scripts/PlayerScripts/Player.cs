using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int maxHealth;
    private Animator anim;
    public HealthBar healthBar;

    private int health;
    
    void Start()
    {  
        health = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        anim = GetComponentInChildren<Animator>();
    }

    public void TakeDamage(int damage)
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
