using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Move")]
    [SerializeField] private float moveSpeedMultiplier;
    private float horizontalInput;

    [Header("Player Jump")]
    [SerializeField] private float jumpingPower;
    [SerializeField] private LayerMask groundLayer;
    private Transform groundCheck;



    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        groundCheck = transform.GetChild(0).GetComponent<Transform>();
    }

    void Update()
    {
        GetInput();
        if (transform.localScale.x > 1 && horizontalInput < 0f || transform.localScale.x < 1 && horizontalInput > 0f)
        {
            FlipSprite();
        }
    }

    private void FixedUpdate()
    {
        MovePlayer();

        if (Input.GetButton("Jump"))
        {
            bool _isGrounded = IsGrounded();
            if (_isGrounded)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
            }

        }
    }

    void GetInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal") * moveSpeedMultiplier;
    }

    void MovePlayer()
    {

        Vector2 _movementDirection = new Vector2(horizontalInput, rb.velocity.y);
        rb.velocity = _movementDirection;

    }

    void FlipSprite()
    {
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, 1);
    }

    bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }
}
