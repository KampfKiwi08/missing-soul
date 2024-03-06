using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Move")]
    [SerializeField] private float moveSpeedMultiplier;
    private float horizontalInput;

    

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        GetInput();
        if(transform.localScale.x > 1 && horizontalInput < 0f || transform.localScale.x < 1 && horizontalInput > 0f)
        {
            FlipSprite();
        }
    }

    private void FixedUpdate()
    {
        MovePlayer();
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
        transform.localScale = new Vector3(transform.localScale.x * -1, 1, 1);
    }

    
}
