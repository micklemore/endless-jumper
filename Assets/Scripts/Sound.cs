using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Sound
{
    [SerializeField]
	public AudioClip clip;

	[SerializeField]
	public string name;

	[Range(0f, 1f)]
	public float volume;

	[Range(.1f, 3f)]
	public float pitch;

	[HideInInspector]
	public AudioSource source;
}
