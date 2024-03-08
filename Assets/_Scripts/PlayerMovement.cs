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
    [SerializeField] private int airJumps;
    private int jumpsLeft;
    [SerializeField] private LayerMask groundLayer;

    [Header("Crouch")]
    [SerializeField] private float crouchSpeedMultiplier;
    private bool isCrouching = false;

    [Header("Roll")]
    [SerializeField] private float rollCooldown;
    private bool isRolling;
    private bool canRoll;




    private Transform groundCheck;
    


    private Animator animator;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        groundCheck = transform.GetChild(0).GetComponent<Transform>();
        jumpsLeft = airJumps;
    }

    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal") * moveSpeedMultiplier;
        animator.SetFloat("Horizontal", Input.GetAxisRaw("Horizontal"));
        FlipSprite();

        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            Jump();
        }   
        if(Input.GetKey(KeyCode.LeftShift)) 
        {
            isCrouching = true;
        }else
        {
            isCrouching = false;
        }
        animator.SetBool("isCrouching", isCrouching);
    }

    #region Movement
    private void FixedUpdate()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        if (isRolling) { return; }
        Vector2 _movementDirection = new Vector2(horizontalInput, rb.velocity.y);
        if(isCrouching) 
        {
            _movementDirection = _movementDirection * crouchSpeedMultiplier;
        }
        rb.velocity = _movementDirection;
    }

    void FlipSprite()
    {
        if (transform.localScale.x > 1 && horizontalInput < 0f || transform.localScale.x < 1 && horizontalInput > 0f) 
        {
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, 1);
        }
        
    }
    #endregion Movement

    #region Jumping
    private void Jump()
    {
        if (isRolling) { return; }
        if (IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
            jumpsLeft = airJumps;
        }
        else if (jumpsLeft > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
            jumpsLeft--;
        }
    }

    bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.02f, groundLayer);
    }
    #endregion Jumping

    #region Roll
    private void Roll()
    {
        isRolling = true;
        canRoll = false;
    }
    private void ResetRoll()
    {

    }
    #endregion Roll
}
