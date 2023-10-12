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

	public static AudioManager instance;

	AudioSource actualAudioSource;

	AudioSource gameMusic;

	bool shouldPlayGameMusic = true;

	bool shouldPlayEffectsAudio = true;

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
		EventHandler.instance.startGameDelegate += PlayGameMusic;
		EventHandler.instance.endGameDelegate += PlayGameOverAudio;

		shouldPlayGameMusic = PlayerPrefs.GetInt("GameAudio") == 1;
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
			Destroy(gameMusic.gameObject);
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
}
