using UnityEngine;
using TMPro;  // Use TextMeshPro for text UI

public class PlayerScore : MonoBehaviour
{
    public TMP_Text scoreText;  // Reference to the UI Text component to display current score
    public TMP_Text highScoreText;  // Reference to the UI Text component to display high score
    private int score = 0;  // The player's current score
    private int highScore = 0;  // The highest score

    void Start()
    {
        // Load the high score from PlayerPrefs when the game starts
        highScore = PlayerPrefs.GetInt("HighScore", 0);  // Default value is 0 if no high score is stored
        UpdateScoreText();  // Display current score
        UpdateHighScoreText();  // Display high score
    }

    // Call this method when the player collects a green ball
    public void CollectGreenBall(GameObject greenBall)
    {
        score++;  // Increase the score
        UpdateScoreText();  // Update the current score UI

        // Check if the current score is a new high score
        if (score > highScore)
        {
            highScore = score;  // Update the high score
            PlayerPrefs.SetInt("HighScore", highScore);  // Save the high score to PlayerPrefs
            UpdateHighScoreText();  // Update the high score UI
        }

        Destroy(greenBall);  // Destroy the collected green ball
    }

    // Update the current score UI text
    private void UpdateScoreText()
    {
        scoreText.text = "Score: " + score.ToString();  // Update the score UI with the current score
    }

    // Update the high score UI text
    private void UpdateHighScoreText()
    {
        highScoreText.text = "High Score: " + highScore.ToString();  // Update high score UI
    }

    // Call this method to reset the score, for example when restarting the level
    public void ResetScore()
    {
        score = 0;
        UpdateScoreText();  // Update score UI
    }
}