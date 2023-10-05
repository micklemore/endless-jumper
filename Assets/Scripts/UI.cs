using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    [SerializeField]
    GameObject gameOverView;

    [SerializeField]
    GameObject gameView;

    [SerializeField]
    Text finalScoreText;

    [SerializeField]
    Text scoreText;

    [SerializeField]
	Text timeText;

    [SerializeField]
    Button restartGameButton;

	[SerializeField]
	Button startGameButton;

	[SerializeField]
	Button quitGameButton;

	string scoreLabel = "Current score: ";
    
    string timeLabel = "Game time: ";

    int startScore = 0;

	float gameTime = 0f;

	void Start()
    {
        SetScoreText(scoreLabel + startScore);

        EventHandler.instance.incrementScoreDelegate += IncrementScore;
        EventHandler.instance.endGameDelegate += ShowGameOverScreen;
    }

	void OnEnable()
	{
        startGameButton.onClick.AddListener(StartGameButtonPressed);
        quitGameButton.onClick.AddListener(QuitGameButtonPressed);
        restartGameButton.onClick.AddListener(RestartGameClicked);
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

    void StartGameButtonPressed()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    void QuitGameButtonPressed()
    {
        Application.Quit();
    }

	public void RestartGameClicked()
    {
        EventHandler.instance.OnRestartGameClicked();
    }
}