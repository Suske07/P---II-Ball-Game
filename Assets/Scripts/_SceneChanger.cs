using UnityEngine;
using UnityEngine.SceneManagement;  // For Scene management

public class _SceneChanger : MonoBehaviour
{
    public string sceneToLoad = "Play Again";  // Name of the scene to load after 30 seconds
    private float timer = 0f;  // Timer to track elapsed time
    private float switchInterval = 30f;  // Time in seconds to wait before changing the scene

    void Update()
    {
        // Increase the timer by the time passed since the last frame
        timer += Time.deltaTime;

        // Check if 30 seconds have passed
        if (timer >= switchInterval)
        {
            // Load the specified scene
            SceneManager.LoadScene(sceneToLoad);

            // Reset the timer to avoid loading multiple times
            timer = 0f;  // Optional, since the scene will change
        }
    }
}