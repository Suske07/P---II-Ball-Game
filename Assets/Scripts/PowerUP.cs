using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public float effectDuration = 10f;  // Duration of the power-up effect
    private bool isActive = false;      // To track if the power-up effect is active
    private float timer = 0f;           // Timer to track how long the effect has been active

    // Reference to the red balls (KillerBalls) in the scene
    private GameObject[] redBalls;

    void Start()
    {
        // Initialize red balls
        redBalls = GameObject.FindGameObjectsWithTag("KillerBall");
    }

    void Update()
    {
        // If the power-up effect is active, start the timer
        if (isActive)
        {
            timer += Time.deltaTime;  // Increment the timer by the time passed since last frame

            // If the timer exceeds the effect duration, reset the red balls to normal
            if (timer >= effectDuration)
            {
                ResetRedBalls();
                isActive = false;  // Deactivate the power-up effect
            }
        }
    }

    // Method to activate the power-up effect
    public void ActivatePowerUp()
    {
        if (isActive) return; // If already active, do nothing

        isActive = true;    // Activate the power-up effect
        timer = 0f;         // Reset the timer when the effect starts

        // Apply the power-up effect to all red balls
        foreach (GameObject redBall in redBalls)
        {
            KillerBall rbScript = redBall.GetComponent<KillerBall>();
            if (rbScript != null)
            {
                rbScript.ApplyPowerUpEffect();  // Apply size and speed reduction
            }
        }
    }

    // Method to reset red balls to their original size and speed after the effect duration
    private void ResetRedBalls()
    {
        foreach (GameObject redBall in redBalls)
        {
            KillerBall rbScript = redBall.GetComponent<KillerBall>();
            if (rbScript != null)
            {
                rbScript.ResetSizeAndSpeed();  // Reset the size and speed of red balls
            }
        }
    }

    // This method is triggered when another collider enters the trigger collider of the PowerUp object
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If the player collides with the power-up object, activate the power-up
        if (collision.CompareTag("Player"))
        {
            ActivatePowerUp();  // Activate the power-up effect
            Destroy(gameObject);  // Destroy the power-up object after use
        }
    }
}