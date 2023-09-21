using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    float secondsToReturnToStartPosition = 3f;

	int currentScore = 0;

	private void Awake()
	{
		if (instance != null)
		{
			Destroy(this);
		}
		instance = this;
	}

	private void Start()
	{
		EventHandler.instance.StartGameNotify();
		EventHandler.instance.restartGameClickedDelegate += RestartGame;
	}

	public float GetSecondsToReturnToStartPosition()
	{
		return secondsToReturnToStartPosition;
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
		SceneManager.LoadScene(0);
	}
}
