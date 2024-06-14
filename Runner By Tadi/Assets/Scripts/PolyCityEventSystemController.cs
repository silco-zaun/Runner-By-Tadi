using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PolyCityEventSystemController : MonoBehaviour
{
    public void ReplayGame()
    {
        AudioManager.Ins.PlaySound("SelectButton");
        SceneManager.LoadScene("PolyCity");
    }

    public void QuitGame()
    {
        AudioManager.Ins.PlaySound("SelectButton");
        Application.Quit();
    }
}
