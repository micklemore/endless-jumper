using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

	[SerializeField]
	float secondsToReturnToStartPosition = 3f;

	[SerializeField]
	float obstacleMinSpawnTimer = 2f;

	[SerializeField]
	float obstacleMaxSpawnTimer = 5f;

	public float SecondsToReturnToStartPosition => secondsToReturnToStartPosition;

	public float MinSpawnTimer => obstacleMinSpawnTimer;

	public float MaxSpawnTimer => obstacleMaxSpawnTimer;

	int currentScore = 0;

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
		EventHandler.instance.StartGameNotify();
		EventHandler.instance.restartGameClickedDelegate += RestartGame;
		EventHandler.instance.endGameDelegate += EndGame;
	}

	public float GetSecondsToReturnToStartPosition()
	{
		return secondsToReturnToStartPosition;
	}

	public void IncrementScore()
	{
		Debug.Log("current score is " + currentScore);
		currentScore++;
		Debug.Log("current score now is " + currentScore);
		EventHandler.instance.IncrementScoreNotify(currentScore);
	}

	public int GetIncrementedScore()
	{
		return ++currentScore;
	}

	public int GetCurrentScore()
	{
		return currentScore;
	}

	void RestartGame()
	{
		Time.timeScale = 1;
		SceneManager.LoadScene(0);
	}

	void EndGame(int score)
	{
		Time.timeScale = 0;
	}
}
