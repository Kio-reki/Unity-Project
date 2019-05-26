using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{

    [Header("Movement")]
    public bool facingRight = true;
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

    public bool canDash = true;
    public float dashDistance = 5;
    public float superDashDistance = 10;
    //public float dashTime;
    //public float startDashTime;
    //public float rightDashSpeed;
    //public float leftDashSpeed;

    //public int direction;


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
    Animator anim;



    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        //dashTime = startDashTime;
    }

    void FixedUpdate()
    {

        PlayerPosition = transform;

        //GROUND CHECK
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
        //anim.SetBool("grounded", grounded);

        if (grounded)
        {
            canDash = true;
        }

        //MOVEMENT
        float move = Input.GetAxis("Horizontal");
        anim.SetFloat("Speed", Mathf.Abs(move));
        rb.velocity = new Vector2((move * maxSpeed), rb.velocity.y);

        //FACING DIRECTION CHECK
        if (move > 0 && !facingRight)
            Flip();
        else if (move < 0 && facingRight)
            Flip();

        //FUNCTIONS
        PlayerJump();
        PlayerDash();
        //SuperDash();






       
       

    }
    //VOID FUNCTIIONS

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    void PlayerJump()
    {

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
        }
    }

    void PlayerDash()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            if (facingRight)
            {
                PlayerPosition.position = transform.TransformPoint(dashDistance, 0, 0);
                canDash = false;
            }
            else if (!facingRight)
            {
                PlayerPosition.position = transform.TransformPoint(dashDistance, 0, 0);
                canDash = false;
            }
        }

    }

    /*void SuperDashInit()
    {
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                rb.velocity = new Vector2(0, 0);
                Invoke("SuperDash", 0.5f);
            }
        }
    }
    void SuperDash()
    {
        rb.velocity = new Vector2(superDashDistance, 0);
    }*/
}