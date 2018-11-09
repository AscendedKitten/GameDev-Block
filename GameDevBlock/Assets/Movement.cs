using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    [SerializeField]
    private KeyCode up, down, left, right;
    private bool leftSwitch, rightSwitch;
    private float moveSpeed = 5f;
    private Rigidbody2D body;
    private Collider2D groundCol;

    // Use this for initialization
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(left))
            leftSwitch = true;
        if (Input.GetKey(right))
            rightSwitch = true;
    }

    void FixedUpdate()
    {
        // TODO: Ground check
        if (true)
        {
            if (Input.GetKey(up))
            {
                body.AddForce(new Vector2(0.0f, 100));
            }

            if (Input.GetKey(down))
            {

            }

            if (leftSwitch)
            {
                body.AddForce(new Vector2(-moveSpeed * 5, 0.0f));
                leftSwitch = false;
            }

            if (rightSwitch)
            {
                body.AddForce(new Vector2(moveSpeed * 5, 0.0f));
                rightSwitch = false;
            }
        }
    }

    public void onTriggerStay2D(Collider2D other)
    {

    }
}
