using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement instance;

    public GameObject projectilePrefab;     // Reference to Projectile
    public Image imageHealthBar;            // Reference to UI Healthbar
    public TMP_Text healthPriceText;        // Reference to shop health text
    public TMP_Text firingPriceText;        // Reference to shop firing text

    public float moveSpeed = 5f;            // Speed of player movement (horizontal)
    public float jumpForce = 10f;           // Force of the jump (if you want to keep jumping functionality)
    public LayerMask groundLayer;           // Ground layer mask to check if player is grounded
    public float firingTimer;               // Delay for firing projectile
    public float fireRate = 2f;             // Save the previous firingTimer
    public float health = 100f;             // Players health amount
    public float healthMax = 100f;          // Players max health amount
    public bool isPaused;                   // If menu showing, pause the game

    private Rigidbody2D rb;                 // Reference to the Rigidbody2D
    private BoxCollider2D boxCollider;      // Reference to the BoxCollider2D
    private float groundCheckDistance = 0.1f; // Distance to check for ground

    private bool isGrounded;                // Flag to check if player is grounded
    private bool showingShop = false;               // Flag to check if shop is visible

    private void Awake()
    {
        instance = this;

        firingTimer = 0;

        // Get references to the Rigidbody2D and BoxCollider2D
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (isPaused)
        {
            return;
        }

        if (health > 0)
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

            firingTimer += Time.deltaTime;

            // Handle shooting projectile
            if (Input.GetKey(KeyCode.Space) && firingTimer >= fireRate)
            {
                FireProjectile();
                firingTimer = 0;
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                if (!showingShop)
                {
                    showingShop = true;
                    MenuController.instance.ShowShopMenu();
                }
                else
                {
                    MenuController.instance.Hide();
                    showingShop = false;
                }
            }
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

    public void FireProjectile()
    {
        Instantiate(projectilePrefab, transform.position + new Vector3(0, 0.8f, 0), Quaternion.identity);
    }

    private void Jump()
    {
        // Apply a vertical force for jumping (optional)
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    void Die()
    {
        MenuController.instance.ShowDeathMenu();
    }

    void TakeDamage(float damageAMount)
    {
        health -= damageAMount;
        if(health <= 0)
        {
            Die();
        }

        imageHealthBar.fillAmount = health / healthMax;
    }

    public void UpgradeHealth()
    {
        int cost = Mathf.RoundToInt(healthMax);

        if(GameController.instance.money >= cost)
        {
            GameController.instance.money -= cost;

            health += 50;
            healthMax += 50;
            imageHealthBar.fillAmount = health / healthMax;

            healthPriceText.text = "Health\n" + Mathf.RoundToInt(healthMax);
        }
    }

    public void UpgradeFireRate()
    {
        int cost = 100 + Mathf.RoundToInt((1f - fireRate) * 100f);

        if (GameController.instance.money >= cost)
        {
            GameController.instance.money -= cost;

            fireRate = Mathf.Max(0.2f, fireRate - 0.05f);

            int newCost = 100 + Mathf.RoundToInt((1f - firingTimer) * 100f);
            firingPriceText.text = "Fire Speed\n" + newCost;

        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.GetComponent<EnemyController>())
        {
            TakeDamage(10f);
            Destroy(other.gameObject);
        }
    }

    private bool IsGrounded()
    {
        // Check if the player's collider is touching the ground
        return Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.down, groundCheckDistance, groundLayer);
    }
}