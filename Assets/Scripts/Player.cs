using UnityEngine;
using UnityEngine.SceneManagement;  // Required for scene management
using System.Collections;  // Required for IEnumerator

public class Player : MonoBehaviour
{
    private PlayerMovement playerMovement;  // Reference to PlayerMovement script
    private PlayerScore playerScore;        // Reference to PlayerScore script
    private GameObject[] redBalls;          // To store all red balls in the scene

    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();  // Get the PlayerMovement component
        playerScore = GetComponent<PlayerScore>();        // Get the PlayerScore component

        // Find all red balls (killer balls) in the scene
        redBalls = GameObject.FindGameObjectsWithTag("KillerBall");
    }

    // Detect collisions with GreenBalls, KillerBalls, and PowerUp
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // If the player collides with a GreenBall, update the score
        if (collision.gameObject.CompareTag("GreenBall"))
        {
            playerScore.CollectGreenBall(collision.gameObject);  // Call method to collect green ball
        }
        // If the player collides with a KillerBall, handle the player's death
        else if (collision.gameObject.CompareTag("KillerBall"))
        {
            playerMovement.Die();  // Call method to handle death

            // Trigger the scene transition after 5 seconds
            StartCoroutine(HandlePlayerDeath());  // Start the coroutine to load the scene after a delay
        }
        // If the player collides with a PowerUp, apply the power-up effect to all red balls
        else if (collision.gameObject.CompareTag("PowerUp"))
        {
            ApplyPowerUpEffect();  // Call the method to apply the power-up effect to all red balls

            // Destroy the power-up object after the player collects it
            Destroy(collision.gameObject);
        }
    }

    // Method to apply the power-up effect to all red balls (KillerBalls)
    private void ApplyPowerUpEffect()
    {
        // Loop through all the red balls and apply the effect
        foreach (GameObject redBall in redBalls)
        {
            KillerBall rbScript = redBall.GetComponent<KillerBall>();  // Get the KillerBall script
            if (rbScript != null)
            {
                rbScript.ApplyPowerUpEffect();  // Apply the power-up effect to each red ball
            }
        }
    }

    // Coroutine to handle player death and load the "Play Again" scene after a delay
    private IEnumerator HandlePlayerDeath()
    {
        // Wait for 5 seconds
        yield return new WaitForSeconds(5f);

        // After 5 seconds, load the "Play Again" scene
        SceneManager.LoadScene("Play Again");  // Load the "Play Again" scene (make sure the name is correct)
    }
}
