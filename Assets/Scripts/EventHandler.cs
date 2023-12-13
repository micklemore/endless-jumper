using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EventHandler : MonoBehaviour
{
    public static EventHandler instance;

	void Awake()
	{
		if (instance != null)
        {
            Destroy(this);
        }
        instance = this;
	}

    public delegate void IncrementScore(int score);
    public IncrementScore incrementScoreDelegate;

    public void IncrementScoreNotify(int score)
    {        
        incrementScoreDelegate?.Invoke(score);
    }

    public delegate void EndGameFinalScore(int finalScore);
    public EndGameFinalScore endGameDelegate;

    public void EndGameNotify()
    {
		endGameDelegate?.Invoke(GameManager.instance.GetCurrentScore());
        GameManager.instance.EndGame();
    }

    public delegate void OnStartGame();
    public OnStartGame startGameDelegate;

    public void StartGameNotify()
    {
        startGameDelegate?.Invoke();
    }

    public delegate void PauseButtonClicked();
    public PauseButtonClicked pauseButtonClickedDelegate;

    public void OnPauseButtonClicked()
    {
        pauseButtonClickedDelegate?.Invoke();
        GameManager.instance.PauseGame();
	}

    public delegate void ResumeButtonClicked();
    public ResumeButtonClicked resumeButtonClickedDelegate;

    public void OnResumeButtonClicked()
    {
        resumeButtonClickedDelegate?.Invoke();
        GameManager.instance.ResumeGame();
    }

    public delegate void PlayJumpAudio();
    public PlayJumpAudio playJumpAudioDelegate;

    public void PlayJumpAudioNotify()
    {
        playJumpAudioDelegate?.Invoke();
    }

	public delegate void PlayHurtAudio();
	public PlayHurtAudio playHurtAudioDelegate;

	public void PlayHurtAudioNotify()
	{
		playHurtAudioDelegate?.Invoke();
	}

	public delegate void PlayDeathAudio();
	public PlayDeathAudio playDeathAudioDelegate;

	public void PlayDeathAudioNotify()
	{
		playDeathAudioDelegate?.Invoke();
	}
}
