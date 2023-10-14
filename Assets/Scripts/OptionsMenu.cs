using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
	[SerializeField]
	Toggle gameMusicToggle;

	[SerializeField]
	Toggle effectsAudioToggle;

	[SerializeField]
	Button backButton;

	void OnEnable()
	{
		backButton.onClick.AddListener(BackButtonClicked);
	}

	void OnDisable()
	{
		backButton.onClick.RemoveListener(BackButtonClicked);
	}

	void Awake()
	{
		SetTogglesCheckValue();
	}

	void SetTogglesCheckValue()
	{
		if (PlayerPrefs.GetInt("GameMusic") == 0)
		{
			gameMusicToggle.isOn = false;
		}
		else if (PlayerPrefs.GetInt("GameMusic") == 1)
		{
			gameMusicToggle.isOn = true;
		}
		if (PlayerPrefs.GetInt("EffectsAudio") == 0)
		{
			effectsAudioToggle.isOn = false;
		}
		else if (PlayerPrefs.GetInt("EffectsAudio") == 1)
		{
			effectsAudioToggle.isOn = true;
		}
	}

	private void BackButtonClicked()
	{
		PlayerPrefs.SetInt("GameMusic", gameMusicToggle.isOn ? 1 : 0);
		PlayerPrefs.SetInt("EffectsAudio", effectsAudioToggle.isOn ? 1 : 0);

		if (SceneManager.GetActiveScene().buildIndex == 0)
		{
			MenuManager.instance.OnBackButtonPressed();
		}
		else if (SceneManager.GetActiveScene().buildIndex == 1)
		{
			UI.instance.OnBackButtonPressed();
		}
	}
}
