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

	public void RestartGame()
	{
		Time.timeScale = 1;
		SceneManager.LoadScene(0);
	}

	public void EndGame()
	{
		Time.timeScale = 0;
	}

	public void PauseGame()
	{
		Time.timeScale = 0;
	}

	public void ResumeGame()
	{
		Time.timeScale = 1;
	}
}
