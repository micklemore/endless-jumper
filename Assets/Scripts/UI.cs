using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UI : Menu
{
    [SerializeField]
    GameObject gameOverView;

    [SerializeField]
    GameObject gameView;

    [SerializeField]
	TextMeshProUGUI finalScoreText;

    [SerializeField]
	TextMeshProUGUI scoreText;

    [SerializeField]
	TextMeshProUGUI timeText;

    [SerializeField]
    Button restartGameButton;

    [SerializeField]
    Button pauseButton;

    [SerializeField]
    PauseMenu pauseMenu;

    [SerializeField]
    Button optionsButton;

    [SerializeField]
    OptionsMenu optionsMenu;

    public static UI instance;

	string scoreLabel = "Score: ";
    
    string timeLabel = "Game time: ";

    int startScore = 0;

	float gameTime = 0f;

	void Awake()
	{
		if (instance != null)
        {
            Destroy(this);
        }
        instance = this;
	}

	void Start()
    {
        SetScoreText(scoreLabel + startScore);

        EventHandler.instance.incrementScoreDelegate += IncrementScore;
        EventHandler.instance.endGameDelegate += ShowGameOverScreen;
	}

	void OnEnable()
	{
        restartGameButton.onClick.AddListener(RestartGameClicked);
        pauseButton.onClick.AddListener(PauseButtonClicked);
        optionsButton.onClick.AddListener(OptionsButtonClicked);
	}

	void Update()
    {
        UpdateGameTime();
	}

    void IncrementScore(int score)
    {
        SetScoreText(scoreLabel + score);
    }

    void SetScoreText(string text)
    {
        scoreText.text = text;
    }

    void UpdateGameTime()
    {
        gameTime += Time.deltaTime;
        
        timeText.text = timeLabel + GetTimeToShowFromGameTime(gameTime);
    }

    string GetTimeToShowFromGameTime(float gameTime)
    {
		if (gameTime >= 60)
		{
			return (gameTime / 60).ToString("00") + ":" + (gameTime % 60).ToString("00");
		}
		else
		{
			return (gameTime % 60).ToString("00");
		}
	}

    void ShowGameOverScreen(int finalScore)
    {
        gameView.SetActive(false);
        gameOverView.SetActive(true);
        finalScoreText.text = "Final Score: " + finalScore;
    }

	public void RestartGameClicked()
    {
		GameManager.instance.RestartGame();
	}

	void PauseButtonClicked()
    {
		EventHandler.instance.OnPauseButtonClicked();
        pauseMenu.gameObject.SetActive(true);
        //chiama la funzione di inizializzazione
	}

    public void ResumeButtonClicked()
    {
		EventHandler.instance.OnResumeButtonClicked();
		pauseMenu.gameObject.SetActive(false);
	}

    void OptionsButtonClicked()
    {
        pauseMenu.gameObject.SetActive(false);
        optionsMenu.gameObject.SetActive(true);
    }

    public void OnBackButtonPressed()
    {
        optionsMenu.gameObject.SetActive(false);
        pauseMenu.gameObject.SetActive(true);
    }
}