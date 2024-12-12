using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // Outlets
    Rigidbody2D _rb;  // Reference to projectile Rigidbody
    public AudioSource _audioSource;
    public AudioClip _audioClip;

    //Methods
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Set speed and max speed of missile
        float acceleration = GameController.instance.missileSpeed / 2f;
        float maxSpeed = GameController.instance.missileSpeed;

        // Accelerate upward
        _rb.AddForce(transform.up * acceleration);

        // Cap max speed
        _rb.velocity = Vector2.ClampMagnitude(_rb.velocity, maxSpeed);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        // Only explode on enemies
        if (other.gameObject.GetComponent<EnemyController>())
        {
            Destroy(other.gameObject);
            Destroy(gameObject);

            GameController.instance.EarnPoints(10);
        }
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
