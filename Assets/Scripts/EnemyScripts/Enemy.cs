using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 100;

    private int currHealth;
    private bool justDamaged;
    private float cooldown = 1f;
    [SerializeField]
    private GameObject[] hitEffects;
    [SerializeField]
    private GameObject bloodSpray;
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
            GameObject newHit = (Instantiate(hitEffects[Random.Range(0, hitEffects.Length)], transform.position, Quaternion.identity));
            GameObject newSpray = (Instantiate(bloodSpray, transform.position, Quaternion.identity));
            Destroy(newHit, 2f);
            Destroy(newSpray, 2f);

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
       
        Destroy(gameObject);
    }
}
