using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
	[SerializeField]
	Button playButton;

	void OnEnable()
	{
		playButton.onClick.AddListener(PlayButtonClicked);
	}

	void OnDisable()
	{
		playButton.onClick.RemoveListener(PlayButtonClicked);
	}

	void PlayButtonClicked()
	{
		SceneManager.LoadScene(1);
	}
}
