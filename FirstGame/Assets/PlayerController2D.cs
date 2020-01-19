using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2D : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rigidBody2D;
    SpriteRenderer spriteRenderer;

    [SerializeField]
    Transform groundCheck;

    [SerializeField]
    Transform groundCheckLeft;

    [SerializeField]
    Transform groundCheckRight;

    [SerializeField]
    float jumpSpeed = 3;

    [SerializeField]
    float runSpeed = 2;

    bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        this.animator = GetComponent<Animator>();
        this.rigidBody2D = GetComponent<Rigidbody2D>();
        this.spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        if(Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground")) ||
           Physics2D.Linecast(transform.position, groundCheckLeft.position, 1 << LayerMask.NameToLayer("Ground")) ||
           Physics2D.Linecast(transform.position, groundCheckRight.position, 1 << LayerMask.NameToLayer("Ground"))) {
            isGrounded = true;
        } else
        {
            isGrounded = false;
        }



        if (Input.GetKey("d") || Input.GetKey("left"))
        {
            rigidBody2D.velocity = new Vector2(-runSpeed, rigidBody2D.velocity.y);
            spriteRenderer.flipX = true;

            if(isGrounded)
            {
                animator.Play("Player_run");
            }

        } else if(Input.GetKey("q") || Input.GetKey("right"))
        {
            rigidBody2D.velocity = new Vector2(runSpeed, rigidBody2D.velocity.y);
            spriteRenderer.flipX = false;

            if (isGrounded)
            {
                animator.Play("Player_run");
            }

        } else
        {
            rigidBody2D.velocity = new Vector2(0, rigidBody2D.velocity.y);

            if (isGrounded)
            {
                animator.Play("Player_idle");
            }
        }

        if(Input.GetKey("z") || Input.GetKey("space") || Input.GetKey("up") && isGrounded)
        {
            rigidBody2D.velocity = new Vector2(rigidBody2D.velocity.x, jumpSpeed);
            animator.Play("Player_jump");
        }
    }
}
