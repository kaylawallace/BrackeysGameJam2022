using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation_Test : MonoBehaviour
{
    private Vector2 moveDirection;
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        moveDirection = new Vector2(moveX, moveY).normalized;

        Animate();
    }

    void Animate()
    {
        anim.SetFloat("AnimMoveX", moveDirection.x);
        anim.SetFloat("AnimMoveY", moveDirection.y);
    }
}
