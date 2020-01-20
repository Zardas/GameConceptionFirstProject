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

    [SerializeField]
    public float topLimit;
    [SerializeField]
    public float leftLimit;
    [SerializeField]
    public float rightLimit;
    [SerializeField]
    public float bottomLimit;


    [SerializeField]
    public GameObject gun;


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



        if (Input.GetKey("d") || Input.GetKey("left")) // LEFT
        {
            if(rigidBody2D.position.x > leftLimit)
            {
                rigidBody2D.velocity = new Vector2(-runSpeed, rigidBody2D.velocity.y);
            }
            
            spriteRenderer.flipX = true;
            gun.GetComponent<SpriteRenderer>().flipY = true;

            if (isGrounded)
            {
                animator.Play("Player_run");
            }





        } else if(Input.GetKey("q") || Input.GetKey("right")) // DROITE
        {
            if (rigidBody2D.position.x < rightLimit)
            {
                rigidBody2D.velocity = new Vector2(runSpeed, rigidBody2D.velocity.y);
            }
                
            spriteRenderer.flipX = false;
            gun.GetComponent<SpriteRenderer>().flipY = false;

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







        // SAUT
        if(Input.GetKey("z") || Input.GetKey("space") || Input.GetKey("up") && isGrounded)
        {
            if (rigidBody2D.position.y < topLimit)
            {
                rigidBody2D.velocity = new Vector2(rigidBody2D.velocity.x, jumpSpeed);
            }  
            animator.Play("Player_jump");
        }
    }



    //Gizmo = élément graphiques dans l'éditeur pour les devs
    private void OnDrawGizmos()
    {
        //Carré pour la caméra
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(new Vector2(leftLimit, topLimit), new Vector2(rightLimit, topLimit));
        Gizmos.DrawLine(new Vector2(leftLimit, topLimit), new Vector2(leftLimit, bottomLimit));
        Gizmos.DrawLine(new Vector2(leftLimit, bottomLimit), new Vector2(rightLimit, bottomLimit));
        Gizmos.DrawLine(new Vector2(rightLimit, bottomLimit), new Vector2(rightLimit, topLimit));
    }
}
