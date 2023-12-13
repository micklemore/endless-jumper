using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{ 
	[SerializeField]
	AudioSource gameMusicAudioSource;

	[SerializeField]
	AudioSource jumpAudioSource;

	[SerializeField]
	AudioSource hurtAudioSource;

	[SerializeField]
	AudioSource gameOverAudioSource;

	[SerializeField]
	AudioSource deathAudioSource;

	AudioSource actualAudioSource;

	AudioSource gameMusic;

	bool shouldPlayGameMusic = true;

	bool shouldPlayEffectsAudio = true;

	void Start()
	{
		EventHandler.instance.startGameDelegate += PlayGameMusic;
		EventHandler.instance.endGameDelegate += PlayGameOverAudio;
		EventHandler.instance.pauseButtonClickedDelegate += PauseAudio;
		EventHandler.instance.resumeButtonClickedDelegate += ResumeAudio;
		EventHandler.instance.playJumpAudioDelegate += PlayJumpAudio;
		EventHandler.instance.playHurtAudioDelegate += PlayHurtAudio;
		EventHandler.instance.playDeathAudioDelegate += PlayDeathAudio;

		shouldPlayGameMusic = PlayerPrefs.GetInt("GameMusic") == 1;
		shouldPlayEffectsAudio = PlayerPrefs.GetInt("EffectsAudio") == 1;
	}

	void Update()
	{
		shouldPlayGameMusic = PlayerPrefs.GetInt("GameMusic") == 1;
		shouldPlayEffectsAudio = PlayerPrefs.GetInt("EffectsAudio") == 1;
	}

	void PlayGameMusic()
	{
		if (shouldPlayGameMusic)
		{
			gameMusic = Instantiate(gameMusicAudioSource, new Vector3(0, 0, 0), Quaternion.identity);
			gameMusic.Play();
		}
	}

	void PlayGameOverAudio(int score)
	{
        if (shouldPlayGameMusic)
        {
			if (gameMusic != null)
			{
				Destroy(gameMusic.gameObject);
			}
			PlaySpecificAudioSource(gameOverAudioSource);
		}
	}

	public void PlayJumpAudio()
	{
		if (shouldPlayEffectsAudio)
		{
			PlaySpecificAudioSource(jumpAudioSource);
		}
	}

	public void PlayHurtAudio()
	{
		if (shouldPlayEffectsAudio)
		{
			PlaySpecificAudioSource(hurtAudioSource);
		}
	}

	public void PlayDeathAudio()
	{
		if (shouldPlayEffectsAudio)
		{
			PlaySpecificAudioSource(deathAudioSource);
		}
	}

	void PlaySpecificAudioSource(AudioSource audioSource)
	{
		actualAudioSource = Instantiate(audioSource, new Vector3(0, 0, 0), Quaternion.identity);
		actualAudioSource.PlayOneShot(audioSource.clip, audioSource.volume);
	}

	void PauseAudio()
	{
		if (actualAudioSource != null)
		{
			actualAudioSource.Pause();
		}
		if (gameMusic != null)
		{
			gameMusic.Pause();
		}
	}

	void ResumeAudio()
	{
		if (shouldPlayGameMusic)
		{
			if (gameMusic != null)
			{
				gameMusic.UnPause();
			}
			else
			{
				PlayGameMusic();
			}
		}
	}
}
