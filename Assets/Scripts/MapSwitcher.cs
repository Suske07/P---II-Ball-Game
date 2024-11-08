using UnityEngine;
using UnityEngine.SceneManagement;  // Import for scene management

public class MapSwitcher : MonoBehaviour
{
    public string scene1Name = "SampleScene";  // Name of the first scene
    public string scene2Name = "SampleScene 1";  // Name of the second scene

    private float timer = 0f;  // Timer to track elapsed time
    private float switchInterval = 30f;  // Interval in seconds between scene switches

    void Update()
    {
        // Increment the timer by the time passed since the last frame
        timer += Time.deltaTime;

        // If 30 seconds have passed, switch scenes
        if (timer >= switchInterval)
        {
            SwitchScene();
            timer = 0f;  // Reset the timer after switching the scene
        }
    }

    void SwitchScene()
    {
        // Switch to the next scene
        if (SceneManager.GetActiveScene().name == scene1Name)
        {
            SceneManager.LoadScene(scene2Name);  // Load the second scene
        }
        else
        {
            SceneManager.LoadScene(scene1Name);  // Load the first scene
        }
    }
}