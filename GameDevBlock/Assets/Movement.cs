using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField]
    private KeyCode up, left, right, down;
    private bool leftSwitch, rightSwitch, upSwitch;
    private float moveSpeed;
    private Rigidbody2D body;
    private Collider2D colBox;
    private bool groundedtr;
    private bool groundedcol;
    private bool onWalltr;
    private bool onWallcol;
    private bool inAir;
    private bool isAbleToJump;

    private float ff; //framerate fix
    private float maxSpeed;


    // Use this for initialization
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        colBox = GetComponent<Collider2D>();
        ff = Time.deltaTime * 50;
        moveSpeed = 5f * ff;
        maxSpeed = 12;
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

        if (groundedcol && isAbleToJump && upSwitch)
        {
            body.velocity = new Vector2(body.velocity.x, 17);
            /*
            if (groundedcol)
                body.AddForce(new Vector2(0.0f, 420));
            else
                body.AddForce(new Vector2(0.0f, 90));
                */
        }

        if (body.velocity.y > 0.9f)
            colBox.sharedMaterial.friction = 0;
        else
            colBox.sharedMaterial.friction = 0.3f;

    }

    void FixedUpdate()
    {
        if (groundedcol)
        {
            body.drag = -0.1f;

            if (leftSwitch && body.velocity.x > -maxSpeed)
            {
                body.AddForce(new Vector2(-moveSpeed * 10, 0.0f));
                leftSwitch = false;
                Debug.Log(body.velocity.magnitude);
            }

            if (rightSwitch && body.velocity.x < maxSpeed)
            {
                body.AddForce(new Vector2(moveSpeed * 10, 0.0f));
                rightSwitch = false;
                Debug.Log(body.velocity.magnitude);
            }
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
                Debug.Log(body.velocity.magnitude);
            }

            if (rightSwitch && body.velocity.magnitude < maxSpeed)
            {
                body.AddForce(new Vector2(moveSpeed * 6f, 0.0f));
                rightSwitch = false;
                Debug.Log(body.velocity.magnitude);
            }
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
