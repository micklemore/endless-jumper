using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
	[SerializeField]
    Button optionsButton;

    [SerializeField]
    MainMenu mainMenu;

    [SerializeField]
    OptionsMenu optionsMenu;

    public static MenuManager instance;
	void Awake()
	{
		if (instance != null)
		{
			Destroy(this);
		}
		instance = this;
	}

	void OnEnable()
	{
		optionsButton.onClick.AddListener(OptionsMenuClicked);
	}

	void OnDisable()
	{
		optionsButton.onClick.RemoveListener(OptionsMenuClicked);
	}

	void Start()
    {
		PlayerPrefs.SetInt("GameMusic", 1);
		PlayerPrefs.SetInt("EffectsAudio", 1);
	}

    void OptionsMenuClicked()
    {
        mainMenu.gameObject.SetActive(false);
        optionsMenu.gameObject.SetActive(true);
    }

    public void OnBackButtonPressed()
    {
		optionsMenu.gameObject.SetActive(false);
		mainMenu.gameObject.SetActive(true);
	}
}
