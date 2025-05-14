using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 8f;
    [SerializeField] private float jumpingPower = 16f;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Animator anim;
    [SerializeField] private AudioManager audioManager; // Audio manager untuk efek suara

    private float horizontal;
    private bool isFacingRight = true;  
    private int jumpCount = 0;
    private int maxJumps = 2;
    public bool gameOver = false;
    private bool isRunning = false;

    void Update()
    {
        if (!gameOver)
        {
            HandleInput();
        }

        // Kurangi tinggi lompatan jika tombol dilepas sebelum puncak
        if (Input.GetButtonUp("Jump") && rb.linearVelocity.y > 0f)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * 0.5f);
        }

        Flip();
    }

    void FixedUpdate()
    {
        if (!gameOver)
        {
            // Gerakan horizontal diatur dalam FixedUpdate untuk physics yang stabil
            rb.linearVelocity = new Vector2(horizontal * speed, rb.linearVelocity.y);
        }
    }

    private void HandleInput()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        HandleMovement();
        HandleJump();
        HandleRunSound();
    }

    private void HandleMovement()
    {
        anim.SetBool("isRun", Mathf.Abs(horizontal) > 0.1f);
        audioManager.PlayRunSound();
    }

    private void HandleJump()
    {
        if (Input.GetButtonDown("Jump") && jumpCount < maxJumps)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpingPower);
            anim.SetBool("isJump", true);
            audioManager.StopRunSound();
            audioManager.PlayJumpSound();
            jumpCount++;
        }
    }

    private void HandleRunSound()
    {
        if (Mathf.Abs(horizontal) > 0.1f && IsGrounded())
        {
            if (!isRunning)
            {
                isRunning = true;
            }
        }
        else
        {
            if (isRunning)
            {
                isRunning = false;
            }
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void Flip()
    {
        if ((isFacingRight && horizontal < 0f) || (!isFacingRight && horizontal > 0f))
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            anim.SetBool("isJump", false);
            jumpCount = 0;
        }

        if (other.gameObject.CompareTag("Enemy"))
        {
            gameOver = true;
            anim.SetBool("isRun", false);
            audioManager.StopRunSound();
            audioManager.PlayDeathSound();
            isRunning = false;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            anim.SetBool("isJump", true);
            audioManager.StopRunSound();
            isRunning = false;
        }
    }
}
