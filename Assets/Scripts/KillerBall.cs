using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillerBall : MonoBehaviour
{
    public float speed = 5f;  // Original speed of the ball
    public float originalSpeed;  // Store the original speed
    public Vector3 originalSize;  // Store the original size

    private Vector2 direction;
    private Rigidbody2D rb;
    private bool isPowerUpActive = false;

    // New variable to determine the type of ball
    public bool followsPlayer;  // True if this ball follows the player, false if it bounces

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        originalSpeed = speed;  // Store the original speed
        originalSize = transform.localScale;  // Store the original size

        // Randomly determine if this ball follows the player or bounces
        followsPlayer = Random.Range(0f, 1f) < 0.5f;

        if (!followsPlayer)
        {
            ChangeDirection();  // Set the initial random direction if not following
        }
    }

    void FixedUpdate()
    {
        if (followsPlayer)
        {
            FollowPlayer();
        }
        else
        {
            // Update velocity based on the current direction
            rb.velocity = direction * speed;
        }
    }

    private void ChangeDirection()
    {
        // Generate a random angle between 0 and 360
        float angle = Random.Range(0f, 360f);
        // Calculate the direction vector based on that angle
        direction = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad)).normalized;
    }

    private void FollowPlayer()
    {
        // Assuming you have a reference to the player
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            // Calculate the direction towards the player
            Vector2 directionToPlayer = (player.transform.position - transform.position).normalized;
            // Set the velocity directly
            rb.velocity = directionToPlayer * speed; // Update velocity to follow the player
        }
    }

    // Detect collision with the player and handle death
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Call the player's die function
            collision.gameObject.SendMessage("Die");
            // Destroy this killer ball after it kills the player
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Wall") && !followsPlayer)
        {
            // Reflect the direction when it hits a wall
            Vector2 normal = collision.contacts[0].normal;
            direction = Vector2.Reflect(direction, normal).normalized;
        }
    }

    // Method to apply the power-up effect (halve size and speed)
    public void ApplyPowerUpEffect()
    {
        if (!isPowerUpActive)
        {
            speed *= 0.5f;  // Halve the speed
            transform.localScale *= 0.5f;  // Halve the size
            isPowerUpActive = true;
        }
    }

    // Method to reset the ball to its original size and speed
    public void ResetSizeAndSpeed()
    {
        speed = originalSpeed;  // Reset speed to original value
        transform.localScale = originalSize;  // Reset size to original value
        isPowerUpActive = false;
    }
}