
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{

    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject startingText;
    [SerializeField] private GameObject newRecordPanel;
    [SerializeField] private Text scoreText;
    [SerializeField] private Text coinText;
    [SerializeField] private Text newRecordText;
    [SerializeField] private GameObject[] characterPrefabs;

    public bool IsGameStarted { get; private set; }
    public int Score { get; private set; }
    public bool IsGamePaused { get; private set; }
    public bool GameOver { get; set; }

    //private AdManager adManager;

    private new void Awake()
    {
        base.Awake();

        int index = PlayerPrefs.GetInt("SelectedCharacter");
        //GameObject go = Instantiate(characterPrefabs[index], transform.position, Quaternion.identity);
        //adManager = FindObjectOfType<AdManager>();
    }

    private void Start()
    {
        Score = 0;
        Time.timeScale = 1;
        GameOver = IsGameStarted = IsGamePaused = false;

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
        if (GameOver)
        {
            Time.timeScale = 0;

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

            gameOverPanel.SetActive(true);
            //Destroy(gameObject);
        }

        //Start Game
        if (MyInputManager.tap && !IsGameStarted)
        {
            IsGameStarted = true;
            //Destroy(startingText);
        }
    }
}
