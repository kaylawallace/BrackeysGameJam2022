using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float dashSpeed = 10f;
    public float startDashTime = 1f;

    private Rigidbody2D rb;
    private Vector2 movInput = Vector2.zero;
    private bool facingRight = true;
    private bool dashed = false, canDash = true;
    private float dashTime;
    private float cooldownTime = 1f;

    private Animator anim;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        dashTime = startDashTime;
    }

    private void FixedUpdate()
    {
        Move();

        if (dashed && canDash)
        {
            Dash();
        }      
    }

    private void Update()
    {
       Animate();
    }

    private void Move()
    {
        rb.MovePosition(rb.position + movInput * speed * Time.fixedDeltaTime);
    }

    private void Dash()
    {
        if (dashTime <= 0)
        {
            //Dash doesn't set to false if player stops holding move half way through
            anim.SetBool("Dash", false);
            dashTime = startDashTime;
            StartCoroutine(Cooldown(cooldownTime));
        }
        else
        {
            anim.SetBool("Dash", true);
            rb.MovePosition(rb.position + movInput * dashSpeed * Time.fixedDeltaTime);
            dashTime -= Time.deltaTime;
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        movInput = context.ReadValue<Vector2>();
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        dashed = context.action.triggered;
        
    }

    IEnumerator Cooldown(float cooldownTime)
    {        
        canDash = false;
        yield return new WaitForSeconds(cooldownTime);
        canDash = true;
    }

    void Animate()
    {
       anim.SetFloat("AnimMoveX", movInput.x);
       anim.SetFloat("AnimMoveY", movInput.y);
       anim.SetFloat("AnimMoveMagnitude", movInput.magnitude);
    }
}
