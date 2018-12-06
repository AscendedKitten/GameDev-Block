﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private KeyCode up, left, right;
    private bool leftSwitch, rightSwitch, upSwitch;
    private Rigidbody2D body;
    private Collider2D colBox;
    private bool groundedtr;
    private bool groundedcol;
    private bool onWalltr;
    private bool onWallcol;
    private bool inAir;
    private bool isAbleToJump;

    [Header("Speed")]
    [SerializeField] [Range(200, 1000)] private float acceleration = 100;
    private float moveSpeed;
    [SerializeField] private float maxSpeed = 12;

    [Header("Jumping")]
    [SerializeField] private float jumpHeight = 25;
    [SerializeField] private float fallMultiplier = 2.5f;
    [SerializeField] private float jumpFallMultiplier = 2f;

    // Use this for initialization
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        colBox = GetComponent<Collider2D>();
        moveSpeed = acceleration * Time.deltaTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(left))
            leftSwitch = true;
        if (Input.GetKey(right))
            rightSwitch = true;
        if (Input.GetKey(up))
            upSwitch = true;
        else
            upSwitch = false;

        //JUMP
        if (groundedcol && isAbleToJump && upSwitch)
            body.velocity = new Vector2(body.velocity.x, jumpHeight);

        if (body.velocity.y < 0f)
            body.velocity += Vector2.up * Physics2D.gravity.y * fallMultiplier * Time.deltaTime;
        else if (body.velocity.y > 0f && !upSwitch)
            body.velocity += Vector2.up * Physics2D.gravity.y * jumpFallMultiplier * Time.deltaTime;

        /*
        if (body.velocity.y > 0.1f)
        {
            colBox.sharedMaterial.friction = 0;
        }
        else
        {
            colBox.sharedMaterial.friction = 1;
        }
        */
    }

    void FixedUpdate()
    {
        if (groundedcol)
        {
            body.drag = 0f;

            if (leftSwitch && body.velocity.x > -maxSpeed)
            {
                body.AddForce(new Vector2(-moveSpeed * 10, 0.0f));
                leftSwitch = false;
            }

            if (rightSwitch && body.velocity.x < maxSpeed)
            {
                body.AddForce(new Vector2(moveSpeed * 10, 0.0f));
                rightSwitch = false;
            }

            Debug.Log(body.velocity.x);
        }

        else if (inAir)
        {
            isAbleToJump = false;
            //Debug.Log("isAbleToJump: 0");
            body.drag = 0.4f;

            if (leftSwitch && body.velocity.x > -maxSpeed)
            {
                body.AddForce(new Vector2(-moveSpeed * 6f, 0.0f));
                leftSwitch = false;
            }

            if (rightSwitch && body.velocity.x < maxSpeed)
            {
                body.AddForce(new Vector2(moveSpeed * 6f, 0.0f));
                rightSwitch = false;
            }

            Debug.Log(body.velocity.x);
        }
        

        else // void state
        {
            if (leftSwitch)
            {
                body.AddForce(new Vector2(-moveSpeed * 20f, 0.0f));
                leftSwitch = false;
            }

            if (rightSwitch)
            {
                body.AddForce(new Vector2(moveSpeed * 20f, 0.0f));
                rightSwitch = false;
            }
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.CompareTag("ground"))
            groundedtr = true;
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("ground"))
            groundedtr = false;
    }

    void OnCollisionStay2D(Collision2D col)
    {
        inAir = false;

        if (col.gameObject.CompareTag("ground"))
            groundedcol = true;
        else if (col.gameObject.CompareTag("wall"))
            onWallcol = true;

        if (!upSwitch)
        {
            isAbleToJump = true;
            //Debug.Log("isAbleToJump: 1");
        }
    }

    void OnCollisionExit2D(Collision2D col)
    {
        inAir = true;

        if (col.gameObject.CompareTag("ground"))
            groundedcol = false;

        if (col.gameObject.CompareTag("wall"))
            onWallcol = false;
    }
}
