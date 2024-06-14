using UnityEngine;
using UnityEngine.SceneManagement;

public class MySceneManager : Singleton<MySceneManager>
{
    void OnEnable()
    {
        // Subscribe to the sceneLoaded event
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        // Unsubscribe from the sceneLoaded event
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // Method to be called when a scene is loaded
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Get the name of the loaded scene
        string sceneName = scene.name;

        // Print the name of the loaded scene to the console
        //Debug.Log("Loaded scene name: " + sceneName);

        switch (sceneName)
        {
            case "PolyCity":
                AudioManager.Ins.PlaySound("PolyCityBGM");
                break;
            default:
                break;
        }
    }
}
