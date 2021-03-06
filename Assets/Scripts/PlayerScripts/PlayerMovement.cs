using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float dashSpeed = 10f;
    public float startDashTime = 1f;
    public bool canMove = true, canDash = true;

    private Rigidbody2D rb;
    private Vector2 movInput = Vector2.zero;
    private bool facingRight = true;
    private bool dashed = false;
    private float dashTime;
    private float cooldownTime = 1f;
    Vector2 dir;

    private Animator anim;
    private AudioManager am;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        am = FindObjectOfType<AudioManager>();
        dashTime = startDashTime;
    }

    private void FixedUpdate()
    {
        //Move();

        if (dashed && canDash)
        {
            dir = movInput;
            Dash();
        }      

        if (canMove)
        {
            Move();
        }
    }

    private void Update()
    {
       Animate();
    }

    private void Move()
    {
        rb.MovePosition(rb.position + movInput * speed * Time.fixedDeltaTime);
        if (movInput.magnitude > 0.1)
        {
            am.PlayLoop("Run");
        }
        else
        {
            am.StopPlaying("Run");
        }
    }

    private void Dash()
    {
        if (dashTime <= 0 || (rb.velocity.x >= 0.15 && rb.velocity.y <= 0.15))
        {
            dashed = false; 
            anim.SetBool("Dash", false);
            dashTime = startDashTime;
            StartCoroutine(Cooldown(cooldownTime));
            canMove = true;
        }
        else
        {
            dashed = true;
            canMove = false; 
            anim.SetBool("Dash", true);
            rb.MovePosition(rb.position + dir * dashSpeed * Time.fixedDeltaTime);
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
        dashed = false;
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
