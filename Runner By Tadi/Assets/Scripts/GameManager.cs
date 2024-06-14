
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    public bool IsGameStarted { get; set; }
    public int Score { get; set; }
    public bool IsGamePaused { get; set; }
    public bool IsGameOver { get; set; }

    //private AdManager adManager;

    private new void Awake()
    {
        int index = PlayerPrefs.GetInt("SelectedCharacter");
        //GameObject go = Instantiate(characterPrefabs[index], transform.position, Quaternion.identity);
        //adManager = FindObjectOfType<AdManager>();
    }

    private void Start()
    {
        //adManager.RequestBanner();
        //adManager.RequestInterstitial();
        //adManager.RequestRewardBasedVideo();
    }

    void Update()
    {
        //Update UI
        //coinText.text = PlayerPrefs.GetInt("TotalGems", 0).ToString();
        //scoreText.text = Score.ToString();

        //Game Over
        if (IsGameOver)
        {
            /*
            if (Score > PlayerPrefs.GetInt("HighScore", 0))
            {
                newRecordPanel.SetActive(true);
                newRecordText.text = "New \nRecord\n" + Score;
                PlayerPrefs.SetInt("HighScore", Score);
            }
            else
            {
                int i = Random.Range(0, 6);

                //if (i == 0)
                //    adManager.ShowInterstitial();
                //else if (i == 3)
                //    adManager.ShowRewardBasedVideo();
            }*/

            //Destroy(gameObject);
        }
    }
}
