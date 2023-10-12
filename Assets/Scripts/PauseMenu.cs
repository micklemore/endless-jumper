using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
	[SerializeField]
	Button resumeButton;

	void OnEnable()
	{
		resumeButton.onClick.AddListener(ResumeButtonClicked);
	}

	void OnDisable()
	{
		resumeButton.onClick.RemoveListener(ResumeButtonClicked);
	}

	void ResumeButtonClicked()
	{
		EventHandler.instance.OnResumeButtonClicked();
		gameObject.SetActive(false);
	}
}
