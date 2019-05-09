using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private KeyCode up, left, right;
    private bool leftSwitch, rightSwitch, upSwitch;
    private Rigidbody2D body;
    private Collider2D colBox;
    private bool grounded;
    private bool onWall;
    private bool inAir;
    private bool isAbleToJump;
    private bool facedRight;

    [Header("Speed")]
    [SerializeField] [Range(200, 1000)]
    private float acceleration = 380;
    private float moveSpeed;
    [SerializeField] private float maxSpeed = 13;

    [Header("Jumping")]
    [SerializeField] private float jumpHeight = 20;
    [SerializeField] private float wallJumpForce = 12;
    [SerializeField] private float fallMultiplier = 2.5f;
    [SerializeField] private float jumpFallMultiplier = 2f;

    [Header("Grappling Hook")]
    public float jumpSpeed = 3f;  
    public bool isSwinging;
    private SpriteRenderer playerSprite;
    private bool isJumping;
    private Animator animator;
    private float jumpInput;
    private float horizontalInput;
    public Vector2 ropeHook;
    public float swingForce = 4f;

    public enum Direction
    {
        bottom,left,right
    }

    // Use this for initialization
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        colBox = GetComponent<Collider2D>();
        playerSprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        moveSpeed = acceleration * Time.deltaTime;
        colBox.sharedMaterial.friction = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        //INPUTS
        if (Input.GetKey(left))
            leftSwitch = true;
        if (Input.GetKey(right))
            rightSwitch = true;
        if (Input.GetKey(up))
            upSwitch = true;
        else
            upSwitch = false;

        //JUMPING
        if (isAbleToJump && Input.GetKeyDown(up))
        {
            if (grounded)
                body.velocity = new Vector2(body.velocity.x, jumpHeight);

            else if (onWall)
            {
                if (facedRight)
                    body.velocity = new Vector2(-wallJumpForce, jumpHeight * 4 / 5);
                else
                    body.velocity = new Vector2(wallJumpForce, jumpHeight * 4 / 5);
            }

        }


        //SHORT/LONG JUMPS, FRICTION
        if (body.velocity.y < 0f)
            body.velocity += Vector2.up * Physics2D.gravity.y * fallMultiplier * Time.deltaTime;
        else if (body.velocity.y > 0f && !upSwitch)
            body.velocity += Vector2.up * Physics2D.gravity.y * jumpFallMultiplier * Time.deltaTime;

        if (body.velocity.y > 0.1f)
            colBox.sharedMaterial.friction = 0;
        else
            colBox.sharedMaterial.friction = 1f;
        
        
    }

    void FixedUpdate()
    {
        //Debug.Log(body.velocity.x);
        //Debug.Log("isAbleToJump: " + isAbleToJump);
        //Debug.Log(body.velocity.y);
        //Debug.Log("FacedRight: " + facedRight);

        if (grounded)
        {
            body.drag = 0f;
            isAbleToJump = true;
            SetSpeedH(10);
        }
        else if (onWall)
        {
            body.drag = 0f;
            isAbleToJump = true;
            SetSpeedH(17);
        }
        else if (inAir)
        {
            body.drag = 0.4f;
            isAbleToJump = false;
            SetSpeedH(6);
        }

        
        if (horizontalInput < 0f || horizontalInput > 0f)
        {
            animator.SetFloat("Speed", Mathf.Abs(horizontalInput));
            playerSprite.flipX = horizontalInput < 0f;
            if (isSwinging)
            {
                animator.SetBool("IsSwinging", true);

                var playerToHookDirection = (ropeHook - (Vector2)transform.position).normalized;

                Vector2 perpendicularDirection;
                if (horizontalInput < 0)
                {
                    perpendicularDirection = new Vector2(-playerToHookDirection.y, playerToHookDirection.x);
                    var leftPerpPos = (Vector2)transform.position - perpendicularDirection * -2f;
                    Debug.DrawLine(transform.position, leftPerpPos, Color.green, 0f);
                }
                else
                {
                    perpendicularDirection = new Vector2(playerToHookDirection.y, -playerToHookDirection.x);
                    var rightPerpPos = (Vector2)transform.position + perpendicularDirection * 2f;
                    Debug.DrawLine(transform.position, rightPerpPos, Color.green, 0f);
                }

                var force = perpendicularDirection * swingForce;
                body.AddForce(force, ForceMode2D.Force);
            }
            else
            {
                animator.SetBool("IsSwinging", false);
                if (grounded)
                {
                    var groundForce = moveSpeed * 2f;
                    body.AddForce(new Vector2((horizontalInput * groundForce - body.velocity.x) * groundForce, 0));
                    body.velocity = new Vector2(body.velocity.x, body.velocity.y);
                }
            }
        }
        else
        {
//            animator.SetBool("IsSwinging", false); 
//            animator.SetFloat("Speed", 0f);
//            When animations are in 
        }

        if (!isSwinging)
        {
            if (!grounded) return;

            isJumping = jumpInput > 0f;
            if (isJumping)
            {
                body.velocity = new Vector2(body.velocity.x, jumpSpeed);
            }
        }
    }

    public void CollEnter(Direction direction)
    {
        switch (direction)
        {
            case Direction.right:
                facedRight = true;
                onWall = true;
                break;

            case Direction.left:
                facedRight = false;
                onWall = true;
                break;

            case Direction.bottom:
                grounded = true;
                break;
        }
        inAir = false;
    }
    public void CollExit(Direction direction)
    {
        switch (direction)
        {
            case Direction.right:
                onWall = false;
                break;

            case Direction.left:
                onWall = false;
                break;

            case Direction.bottom:
                grounded = false;
                break;
        }
        if (!grounded && !onWall)
            inAir = true;
    }

    private void SetSpeedH(float x)
    {
        if (leftSwitch && body.velocity.x >= -maxSpeed)
        {
            body.AddForce(new Vector2(-moveSpeed * x, 0.0f));
            leftSwitch = false;
        }

        if (rightSwitch && body.velocity.x <= maxSpeed)
        {
            body.AddForce(new Vector2(moveSpeed * x, 0.0f));
            rightSwitch = false;
        }
    }

}
