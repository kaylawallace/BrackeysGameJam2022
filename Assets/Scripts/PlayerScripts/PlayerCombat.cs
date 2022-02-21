using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombat : MonoBehaviour
{
    public Animator anim;
    public Transform attackPoint;
    public Collider2D swordCollider;
    public float attackRange = .5f;
    public LayerMask enemyLayers;

    private bool attackLight = false, attackHeavy = false;
    private int lightDamage = 25, heavyDamage = 50;
    private int currDamage; 

    private void Start()
    {
        swordCollider.enabled = false;
        currDamage = lightDamage;
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
    }

    private void LightAttack()
    {
        // Play attack anim here 
        // anim.SetTrigger("LightAttack");

        // this will be done in anim eventually
        swordCollider.enabled = true;
    }

    private void HeavyAttack()
    {
        // Play attack anim here 
        // anim.SetTrigger("LightAttack");

        // this will be done in anim eventually
        swordCollider.enabled = true;
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
