using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // Outlet
    Rigidbody2D _rb;

    // State Tracking
    public int randomAI;

    float randomSpeed;
    Transform target;
    float angle;
    Vector2 moveDirection;
    Vector3 playerDirection;
    bool stop;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        randomSpeed = Random.Range(0.5f, 3f);
        randomAI = Random.Range(0, 4);
        target = GameObject.FindWithTag("Player").transform;
        stop = false;
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

        // Move straight down once past the player
        else if (transform.position.y < -3)
        {
            _rb.velocity = Vector2.down * randomSpeed;
        }

    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
