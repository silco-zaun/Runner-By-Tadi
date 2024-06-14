
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuEventSystemController : MonoBehaviour
{
    public void PlayGame()
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
