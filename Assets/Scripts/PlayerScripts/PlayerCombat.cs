using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombat : MonoBehaviour
{
    private Animator anim;
    public Transform attackPoint;
    public Collider2D swordCollider;
    public float attackRange = .5f;
    public LayerMask enemyLayers;

    private bool attackLight = false, attackHeavy = false;
    private int lightDamage = 25, heavyDamage = 50;
    private int currDamage;
    private float attackCooldown = 0f;

    private void Start()
    {
        swordCollider.enabled = false;
        currDamage = lightDamage;
        anim = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if (attackLight)
        {
            currDamage = lightDamage;
            LightAttack();
        }  
        else if (attackHeavy)
        {
            currDamage = heavyDamage;
            HeavyAttack();
        }
        else
        {
            swordCollider.enabled = false; 
        }

        if (attackCooldown > 0)
        {
            attackCooldown -= Time.deltaTime;
        }
    }

    private void LightAttack()
    {
        if(attackCooldown <= 0)
        {
            anim.SetTrigger("LightAttack");
            attackCooldown = 1f;
        }
        
    }

    private void HeavyAttack()
    {
        if (attackCooldown <= 0)
        {
            anim.SetTrigger("HeavyAttack");
            attackCooldown = 1f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<Enemy>().TakeDamage(currDamage);
        }
    }

    public void OnLightAttack(InputAction.CallbackContext context)
    {
        attackLight = context.action.triggered;
    }

    public void OnHeavyAttack(InputAction.CallbackContext context)
    {
        attackHeavy = context.action.triggered;
    }
}
