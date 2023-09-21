using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
	[SerializeField]
	Sound[] sounds;

	public static AudioManager instance;

	Sound currentAudioPlaying = null;

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
		InitializeSounds();

		EventHandler.instance.startGameDelegate += StartGameMusic;
		EventHandler.instance.endGameAudioDelegate += GameOver;
	}

	void InitializeSounds()
	{
		foreach (Sound s in sounds)
		{
			s.source = gameObject.AddComponent<AudioSource>();
			s.source.volume = s.volume;
			s.source.pitch = s.pitch;
			s.source.clip = s.clip;
		}
	}
	void Play(string name)
	{
		//find in sounds, return a sound that have name equals to name
		Sound s = Array.Find(sounds, sound => sound.name == name);
		currentAudioPlaying = s;
		s.source.Play();
	}

	void StartGameMusic()
	{
		//Play("GameMusic");
	}

	void GameOver()
	{
		StopCurrentAudio();
	}

	void StopCurrentAudio()
	{
		if (currentAudioPlaying != null)
		{
			currentAudioPlaying.source.Stop();
		}
	}
}
