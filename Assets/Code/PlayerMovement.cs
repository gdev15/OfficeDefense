using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;            // Speed of player movement (horizontal)
    public float jumpForce = 10f;           // Force of the jump (if you want to keep jumping functionality)
    public LayerMask groundLayer;           // Ground layer mask to check if player is grounded

    private Rigidbody2D rb;                 // Reference to the Rigidbody2D
    private BoxCollider2D boxCollider;      // Reference to the BoxCollider2D
    private float groundCheckDistance = 0.1f; // Distance to check for ground

    private bool isGrounded;                // Flag to check if player is grounded

    private void Awake()
    {
        // Get references to the Rigidbody2D and BoxCollider2D
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        // Check if the player is grounded (if you want to keep the jump functionality)
        isGrounded = IsGrounded();

        // Handle horizontal movement (left and right)
        float horizontalInput = Input.GetAxis("Horizontal"); // A/D or Left/Right arrow keys
        Move(horizontalInput); // Pass only horizontal input

        // Handle jumping (optional)
        if (Input.GetButtonDown("Jump") && isGrounded) // Space bar or other jump input
        {
            Jump();
        }
    }

    private void Move(float horizontalInput)
    {
        // Move the player horizontally
        Vector2 velocity = rb.velocity;

        // Apply horizontal movement (no vertical movement)
        velocity.x = horizontalInput * moveSpeed;

        // Apply the updated velocity to the Rigidbody2D
        rb.velocity = velocity;

        // Optional: Flip the player sprite based on horizontal movement direction (left/right)
        if (horizontalInput != 0)
        {
            transform.localScale = new Vector3(Mathf.Sign(horizontalInput), 1, 1);
        }
    }

    private void Jump()
    {
        // Apply a vertical force for jumping (optional)
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    private bool IsGrounded()
    {
        // Check if the player's collider is touching the ground
        return Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.down, groundCheckDistance, groundLayer);
    }
}