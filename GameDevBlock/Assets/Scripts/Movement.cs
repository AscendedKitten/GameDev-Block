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
    private bool isAbleToJump;
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
        if (Input.acceleration.x < 0)
            leftSwitch = true;
        if (Input.acceleration.x > 0)
            rightSwitch = true;


        var fingerCount = 0;
        foreach (var touch in Input.touches)
        {
            if (touch.phase != TouchPhase.Ended && touch.phase != TouchPhase.Canceled)
                fingerCount++;
        


        }
        if (fingerCount > 0)
            upSwitch = true;
        else
            upSwitch = false;

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            
            if(touch.phase == TouchPhase.Began)
                jumpBuffer = true;

        }
        //JUMPING
        if (Input.GetKeyDown(up))
            jumpBuffer = true;

        if (isAbleToJump && jumpBuffer)
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
