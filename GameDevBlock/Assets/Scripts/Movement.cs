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
    private bool jumpBuffer;
    private bool facedRight;

    [Header("Speed")]
    [SerializeField] [Range(200, 1000)] private float acceleration = 380;
    private float moveSpeed;
    [SerializeField] private float maxSpeed = 13;

    [Header("Jumping")]
    [SerializeField] private float jumpHeight = 20;
    [SerializeField] private float wallJumpForce = 12;
    [SerializeField] private float fallMultiplier = 2.5f;
    [SerializeField] private float jumpFallMultiplier = 2f;

    int maxJumps = 1;
    int currentJumps = 0;
    int maxWalljumps = 2;
    int currentWalljumps = 0;

    public enum Direction
    {
        bottom,left,right
    }

    


    // Use this for initialization
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        colBox = GetComponent<Collider2D>();
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
        if (Input.GetKeyDown(up))
            jumpBuffer = true;

        if (currentJumps < maxJumps && jumpBuffer)
        {
            if (grounded) {
                currentJumps += 1;
                grounded = false;
                onWall = false;
                body.velocity = new Vector2(body.velocity.x, jumpHeight);

            }
            else if (onWall)
            {

                if(currentWalljumps < maxWalljumps) {

                    currentWalljumps++;

                    if (facedRight)
                        body.velocity = new Vector2(-wallJumpForce, jumpHeight * 4 / 5);
                    else
                        body.velocity = new Vector2(wallJumpForce, jumpHeight * 4 / 5);
            
                }

            }
            jumpBuffer = false;
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

        if (grounded)
        {
            currentJumps = 0;
            currentWalljumps = 0;
            body.drag = 0f;
            SetSpeedH(10);
            
        }
        else if (onWall)
        {
            currentJumps = 0;
            body.drag = 0f;
            SetSpeedH(17);
        }
        else if (inAir)
        {
            body.drag = 0.4f;
            SetSpeedH(6);
        }
        else // void state
        {
            SetSpeedH(20);
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
