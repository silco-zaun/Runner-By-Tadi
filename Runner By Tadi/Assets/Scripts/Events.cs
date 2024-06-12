using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Events : Singleton<Events>
{

    public void ReplayGame()
    {
        SceneManager.LoadScene("PolyCity");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
