using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // Outlet
    Rigidbody2D _rb;                      // Reference to Rigidbody of Enemy 

    // State Tracking
    public int randomAI;

    float randomSpeed;                    // Speed of the enemy
    bool stop = false;                    // Boolean to only run snippets of code once
    
    Vector2 moveDirection;                // Direction the enemy will move
    Vector3 playerDirection;              // Where the AI thinks the player is

    Transform target;                     // Reference to the players transform

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>(); // Get the Rigidbody of the Enemy Sprite
    }

    void Start()
    {
        randomSpeed = Random.Range(0.5f, 3f); // Set a random speed
        randomAI = Random.Range(0, 4); // Set a random AI
        target = GameObject.FindWithTag("Player").transform; // Get the transform of the player object
    }

    void FixedUpdate()
    {
        if (transform.position.y > -3)
        {
            if (randomAI == 0)
            {
                // Always go towards the player 
                playerDirection = (target.position - transform.position).normalized;
                moveDirection = playerDirection * randomSpeed;
                _rb.velocity = moveDirection.normalized * randomSpeed;
                //Debug.DrawLine(transform.position, target.position, Color.red);
            }
            else if (randomAI == 1)
            {
                // No special AI, just move down in a straight line
                _rb.velocity = Vector2.down * randomSpeed;
            }
            else if (randomAI == 2)
            {
                // Only move to the location the player was on spawn
                if (stop == false)
                {
                    playerDirection = (target.position - transform.position).normalized;
                    moveDirection = playerDirection * randomSpeed;
                    stop = true;
                }
                _rb.velocity = moveDirection.normalized * randomSpeed;
                //Debug.DrawLine(transform.position, target.position, Color.blue);
            }
            else if (randomAI == 3)
            {
                // Move in a random angle straight line
                if (stop == false)
                {
                    playerDirection = new Vector3(Random.Range(-0.7f, 0.7f), -1, 0);
                    moveDirection = playerDirection * randomSpeed; ;
                    stop = true;
                }
                _rb.velocity = moveDirection.normalized * randomSpeed;
                //Debug.DrawLine(transform.position, target.position, Color.green);
            }
        }
        else if (transform.position.y < -3.5f) // Move straight down once past the player
        {
            _rb.velocity = Vector2.down * randomSpeed;
        }
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
