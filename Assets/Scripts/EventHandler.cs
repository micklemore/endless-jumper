using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EventHandler : MonoBehaviour
{
    public static EventHandler instance;

	private void Awake()
	{
		if (instance != null)
        {
            Destroy(this);
        }
        instance = this;
	}

    public delegate void IncrementScore(int score);
    public IncrementScore incrementScoreDelegate;

    public void IncrementScoreNotify()
    {
        Debug.Log("Vado a notificare");
        
        incrementScoreDelegate?.Invoke(GameManager.instance.GetIncrementedScore());
    }

    public delegate void EndGameFinalScore(int finalScore);
    public EndGameFinalScore endGameDelegate;

    public delegate void EndGameAudio();
    public EndGameAudio endGameAudioDelegate;

    public void EndGameNotify()
    {
        //endGameDelegate?.Invoke(GameManager.instance.GetCurrentScore());
        endGameAudioDelegate?.Invoke();
    }

    public delegate void OnStartGame();
    public OnStartGame startGameDelegate;

    public void StartGameNotify()
    {
        startGameDelegate?.Invoke();
    }

    public delegate void RestartGameClicked();
    public RestartGameClicked restartGameClickedDelegate;

    public void OnRestartGameClicked()
    {
        restartGameClickedDelegate?.Invoke();
    }
}
