using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 100;

    private int currHealth;
    private bool justDamaged;
    private float cooldown = 1f;
    private Animator anim;

    void Start()
    {
        currHealth = maxHealth;
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
            justDamaged = true;
            currHealth -= damage;
            anim.SetTrigger("Damage");

            // Play hurt anim
            print("damaged");

            if (currHealth <= 0)
            {
                Die();
            }
        }
        
    }

    private void Die()
    {
        Debug.Log("Enemy died");

        // Die anim 
        // Remove enemy
    }
}
