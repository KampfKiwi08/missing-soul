using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumping : MonoBehaviour
{
    [Header("Player Jump")]
    [SerializeField] private float jumpingPower;
    [SerializeField] private LayerMask groundLayer;
    private Transform groundCheck;

    private Rigidbody2D rb;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        groundCheck = transform.GetChild(0).GetComponent<Transform>();
    }

    private void FixedUpdate()
    {
        bool _isGrounded = IsGrounded();

        if (Input.GetButton("Jump"))
        {
            if(_isGrounded)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
            }
            
        }
    }

    bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }
}