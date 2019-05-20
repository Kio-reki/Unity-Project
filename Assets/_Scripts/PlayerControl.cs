using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{

    [Header("Movement")]
    public float maxSpeed = 8f;
    public float currentSpeed;
    public float airSpeed;
    public float jumpForce = 100f;
    public float dblJumpForce = 100f;
    //bool isJumping = false;
    bool canDblJump = false;
    public bool isSliding = false;
    public bool isDashing = false;
    bool isGrappling = false;
    public int jumpCount = 0;
    //public float sprintSpeed = 50f;

    [Space]

    public Transform PlayerPosition;
    public bool grounded = false;

    [Space]

    [Header("GroundCheck")]
    public Transform groundCheck;
    public float groundRadius = 0.2f;
    public LayerMask whatIsGround;

    public Vector2 savedVelocity;

    private Rigidbody2D rb;
    //Animator anim;



    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // anim = GetComponent<Animator>();
        PlayerPosition = transform;
    }

    private void Update()
    {
        if (grounded && jumpCount > 0)
        {
            jumpCount = 0;
        }
    }


    void FixedUpdate()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
        //anim.SetBool("grounded", grounded);

        //anim.SetFloat("vSpeed", rb.velocity.y);


        float move = Input.GetAxis("Horizontal");
        //anim.SetFloat("Speed", Mathf.Abs(move));
        rb.velocity = new Vector2((move * maxSpeed), rb.velocity.y);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (grounded)
            {
                rb.velocity = new Vector2(0, 0);
                rb.AddForce(new Vector2(0, jumpForce));
                canDblJump = true;
            }
            else
            {
                if (canDblJump)
                {
                    canDblJump = false;
                    rb.velocity = new Vector2(0, 0);
                    rb.AddForce(new Vector2(0, jumpForce));
                }
            }



            /* if (grounded && Input.GetKeyDown(KeyCode.Space))
             {
                 rb.AddForce(new Vector2(0, jumpForce));
                 jumpCount += 1;
             }

             if (!grounded && Input.GetKeyDown(KeyCode.Space))
             {
                 rb.AddForce(new Vector2(0, dblJumpForce));
             }*/







        }
    }
}