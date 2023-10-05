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
		EventHandler.instance.startGameDelegate += StartGameMusic;
		EventHandler.instance.endGameDelegate += StopGameMusic;
	}

	void StartGameMusic()
	{
		gameMusicAudioSource.PlayOneShot(gameMusicAudioSource.clip, gameMusicAudioSource.volume);
	}

	void StopAllMusic()
	{
		gameMusicAudioSource.Stop();
		jumpAudioSource.Stop();
		hurtAudioSource.Stop();
		deathAudioSource.Stop();
	}
	void StopGameMusic(int score)
	{
		StopAllMusic();
		gameOverAudioSource.PlayOneShot(gameOverAudioSource.clip, gameOverAudioSource.volume);
	}

	public void PlayJumpAudio()
	{
		jumpAudioSource.PlayOneShot(jumpAudioSource.clip, jumpAudioSource.volume);
	}

	public void PlayHurtAudio()
	{
		hurtAudioSource.PlayOneShot(hurtAudioSource.clip, hurtAudioSource.volume);
	}

	public void PlayDeathAudio()
	{
		deathAudioSource.PlayOneShot(deathAudioSource.clip, deathAudioSource.volume);
	}
}
