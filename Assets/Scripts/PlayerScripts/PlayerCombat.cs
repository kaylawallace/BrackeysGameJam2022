using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombat : MonoBehaviour
{
    private Animator anim;
    private AudioManager am;
    public Transform attackPoint;
    public Collider2D swordCollider;
    public float attackRange = .5f;
    public LayerMask enemyLayers;

    private bool attackLight = false, attackHeavy = false;
    private int lightDamage = 25, heavyDamage = 50;
    public int currDamage;
    private float attackCooldown = 0f;

    private void Start()
    {
        swordCollider.enabled = false;
        currDamage = lightDamage;
        anim = GetComponentInChildren<Animator>();
        am = FindObjectOfType<AudioManager>();
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
            StartCoroutine(SetCanMove());
            attackCooldown = 1f;
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

    IEnumerator SetCanMove()
    {
        GetComponent<PlayerMovement>().canMove = false;
        yield return new WaitForSeconds(0.75f);
        GetComponent<PlayerMovement>().canMove = true;
    }
}
