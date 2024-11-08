using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 10.5f;
    private Rigidbody2D rb;

    // Reference to the Sprite Renderer (for switching sprites)
    private SpriteRenderer spriteRenderer;

    // Sprites for idle, walking, and death
    public Sprite idleSprite;
    public Sprite walkSprite;
    public Sprite deathSprite;  // Sprite to show when player dies

    private bool isDead = false;  // To track if the player is dead
    private bool isWalking = false;  // To track if the player is walking

    // Death animation timing
    private float deathTimer = 2.0f;  // How long the death animation lasts
    private float deathTimeCount = 0f;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();

        // Set initial sprite to idle
        spriteRenderer.sprite = idleSprite;
    }

    void Update()
    {
        if (isDead)
        {
            // If player is dead, play death animation for a certain time
            deathTimeCount += Time.deltaTime;
            if (deathTimeCount >= deathTimer)
            {
                // After the death animation is done, disable the player or handle Game Over
                gameObject.SetActive(false);  // Temporarily disable the player object
                // Alternatively, reload the scene or trigger a Game Over screen
            }
            return;  // Skip the rest of the update logic if the player is dead
        }

        // Handle player movement only if not dead
        Vector2 movement = Vector2.zero;

        if (Input.GetKey("w"))
        {
            movement.y += 1;
        }
        if (Input.GetKey("s"))
        {
            movement.y -= 1;
        }
        if (Input.GetKey("d"))
        {
            movement.x += 1;
        }
        if (Input.GetKey("a"))
        {
            movement.x -= 1;
        }

        // Move the player
        rb.MovePosition(rb.position + movement.normalized * speed * Time.deltaTime);

        // If there is movement, switch to walking animation
        if (movement.magnitude > 0)
        {
            isWalking = true;
            spriteRenderer.sprite = walkSprite;  // Set walking sprite
        }
        else
        {
            isWalking = false;
            spriteRenderer.sprite = idleSprite;  // Set idle sprite
        }
    }

    // Handle the player's death
    public void Die()
    {
        if (isDead) return;  // If already dead, do nothing

        isDead = true;  // Set the player as dead
        spriteRenderer.sprite = deathSprite;  // Switch to death sprite
        Debug.Log("Player collided with a killer ball! Game Over.");
    }

    public void SetWalkingState(bool isWalking)
    {
        this.isWalking = isWalking;
    }
}