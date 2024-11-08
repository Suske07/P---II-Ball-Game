using UnityEngine;
using UnityEngine.UI;  // To work with UI elements like Button

public class QuitButtonHandler : MonoBehaviour
{
    // This function will be called when the button is clicked
    public void OnQuitButtonClick()
    {
        // Log a message for debugging purposes
        Debug.Log("Quit button clicked");

        // Quit the application
        Application.Quit();

        // If running in the editor, stop play mode as well
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}