using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Move")]
    [SerializeField] private float moveSpeedMultiplier;
    private float currentMoveSpeedMultiplier;
    private float horizontalInput;

    [Header("Player Jump")]
    [SerializeField] private float jumpingPower;
    [SerializeField] private int doubleJumps;
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

        jumpsLeft = doubleJumps;

        currentMoveSpeedMultiplier = moveSpeedMultiplier;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            Jump();
        }   

        if(Input.GetKeyDown(KeyCode.LeftShift)) 
        {
            StartCrouching();
        }
        else if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            StopCrouching();
        }
        
        animator.SetFloat("Horizontal", Input.GetAxisRaw("Horizontal"));
        animator.SetBool("isCrouching", isCrouching);
        
        FlipSprite();
    }
    private void FixedUpdate()
    {
        MovePlayer();
    }

    #region Movement

    void MovePlayer()
    {
        if (isRolling) { return; }

        horizontalInput = Input.GetAxisRaw("Horizontal") * currentMoveSpeedMultiplier;

        Vector2 _movementDirection = new Vector2(horizontalInput, rb.velocity.y);
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
            //Ground Jump
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
            jumpsLeft = doubleJumps;
        }
        else if (jumpsLeft > 0)
        {
            //Double Jump
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
            jumpsLeft--;
        }
    }

    bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.02f, groundLayer);
    }
    #endregion Jumping

    #region Crouching
    private void StartCrouching()
    {
        isCrouching = true;
        currentMoveSpeedMultiplier = crouchSpeedMultiplier;

        BoxCollider2D _playerCollider = transform.GetComponent<BoxCollider2D>();

        _playerCollider.size = new Vector2(0.64f, 0.45f);
        _playerCollider.offset = new Vector2(0.04f, -0.1f);
    }

    private void StopCrouching()
    {
        isCrouching = false;
        currentMoveSpeedMultiplier = moveSpeedMultiplier;

        BoxCollider2D _playerCollider = transform.GetComponent<BoxCollider2D>();
        _playerCollider.size = new Vector2(0.41f, 0.64f);
        _playerCollider.offset = new Vector2(-0.01f, -0.01f);
    }

    #endregion Crouching

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
