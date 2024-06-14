using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PolyCityUISystemController : MonoBehaviour
{
    [SerializeField] private GameObject gameStartPanel;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject newRecordPanel;
    [SerializeField] private Text scoreText;
    [SerializeField] private Text coinText;
    [SerializeField] private Text newRecordText;
    [SerializeField] private GameObject[] characterPrefabs;

    // Start is called before the first frame update
    private void Start()
    {
        Init();
    }

    // Update is called once per frame
    private void Update()
    {
        // Game Start
        if (MyInputManager.Ins.tap && !GameManager.Ins.IsGameStarted)
        {
            GameStart();
        }

        scoreText.text = "SCORE : " + GameManager.Ins.Score;
    }

    private void Init()
    {
        PlayerController.Ins.ResetPlayer();
        GameManager.Ins.IsGameStarted = false;
        GameManager.Ins.IsGameOver = false;
        GameManager.Ins.IsGamePaused = false;
        GameManager.Ins.Score = 0;
        Time.timeScale = 1;
    }

    private void GameStart()
    {
        AudioManager.Ins.PlaySound("GameStart");
        GameManager.Ins.IsGameStarted = true;
        gameStartPanel.SetActive(false);
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        GameManager.Ins.IsGameOver = true;
        gameOverPanel.SetActive(true);
    }
}
