using System.Collections;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    public GameObject powerUpPrefab;  // Reference to the Power-Up prefab
    public float spawnInterval = 10f;  // Interval between power-up spawns (in seconds)

    void Start()
    {
        // Start the spawning coroutine to spawn power-ups every X seconds
        StartCoroutine(SpawnPowerUp());
    }

    // Coroutine to spawn power-ups randomly at intervals
    IEnumerator SpawnPowerUp()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);  // Wait for the specified interval

            // Get the screen bounds in world space to spawn within
            Vector2 screenBounds = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
            float x = Random.Range(-screenBounds.x, screenBounds.x);
            float y = Random.Range(-screenBounds.y, screenBounds.y);
            Vector2 spawnPosition = new Vector2(x, y);

            // Instantiate the power-up prefab at a random position within screen bounds
            Instantiate(powerUpPrefab, spawnPosition, Quaternion.identity);
        }
    }
}