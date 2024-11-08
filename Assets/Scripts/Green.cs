using UnityEngine;

public class Green : MonoBehaviour
{
    public float speed = 5f; // Speed of movement
    private Vector2 direction;
    private Rigidbody2D rb;

    public GameObject greenPrefab; // Reference to the Green prefab
    public GameObject killerPrefab; // Reference to the "killer" ball prefab
    public GameObject player; // Reference to the player object

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ChangeDirection(); // Set the initial random direction
    }

    void Update()
    {
        // Update velocity based on the current direction
        rb.velocity = direction * speed;
    }

    private void ChangeDirection()
    {
        // Generate a random angle between 0 and 360
        float angle = Random.Range(0f, 360f);

        // Calculate the direction vector based on that angle
        direction = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad)).normalized;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            // Reflect the direction when it hits a wall
            Vector2 normal = collision.contacts[0].normal;
            direction = Vector2.Reflect(direction, normal).normalized;
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            // Destroy the original green ball upon collision with the player
            Destroy(gameObject);

            // Spawn two new balls (one green and one killer)
            SpawnNewBalls();
        }
    }

    private void SpawnNewBalls()
    {
        // Get the camera's viewport bounds to spawn within the screen
        Vector2 screenBounds = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        float x = Random.Range(-screenBounds.x, screenBounds.x);
        float y = Random.Range(-screenBounds.y, screenBounds.y);
        Vector2 spawnPosition = new Vector2(x, y);

        // Spawn the first green ball (this behaves like the original ball)
        GameObject newGreen = Instantiate(greenPrefab, spawnPosition, Quaternion.identity);
        Green newGreenScript = newGreen.GetComponent<Green>();
        if (newGreenScript != null)
        {
            newGreenScript.ChangeDirection(); // Randomize the direction for the new green ball
            Rigidbody2D newRb = newGreen.GetComponent<Rigidbody2D>();
            newRb.velocity = newGreenScript.direction * newGreenScript.speed; // Apply the velocity
        }

        // Spawn the second "killer" ball (this one kills the player on contact)
        GameObject newKillerBall = Instantiate(killerPrefab, spawnPosition, Quaternion.identity);
        KillerBall killerScript = newKillerBall.GetComponent<KillerBall>(); // Assuming you create a script for this
        if (killerScript != null)
        {
            // The killer ball is already set to behave like the green ball, so no additional setup is needed here
        }
    }
}