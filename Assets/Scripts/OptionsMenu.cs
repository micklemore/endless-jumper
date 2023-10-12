using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
	[SerializeField]
	Toggle gameAudioToggle;

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

	private void BackButtonClicked()
	{
		PlayerPrefs.SetInt("GameAudio", gameAudioToggle.isOn ? 1 : 0);
		PlayerPrefs.SetInt("EffectsAudio", effectsAudioToggle.isOn ? 1 : 0);

		MenuManager.instance.OnBackButtonPressed();
	}
}
