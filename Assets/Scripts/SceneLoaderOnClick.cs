using UnityEngine;
using UnityEngine.SceneManagement;  // To use SceneManager for scene loading
using UnityEngine.UI;  // To interact with UI elements

public class SceneLoaderOnClick : MonoBehaviour
{
    public string sceneName = "SampleScene";  // Name of the scene to load

    // You can attach this method to a button or image click event
    public void OnImageClick()
    {
        // Load the specified scene
        SceneManager.LoadScene(sceneName);
    }
}