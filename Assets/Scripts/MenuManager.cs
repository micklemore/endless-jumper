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
    GameObject mainMenu;

    [SerializeField]
    GameObject optionsMenu;

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
		PlayerPrefs.SetInt("GameAudio", 1);
		PlayerPrefs.SetInt("EffectsAudio", 1);
	}

    void OptionsMenuClicked()
    {
        mainMenu.SetActive(false);
        optionsMenu.SetActive(true);
    }

    public void OnBackButtonPressed()
    {
		optionsMenu.SetActive(false);
		mainMenu.SetActive(true);

		Debug.Log("game audio is " + PlayerPrefs.GetInt("GameAudio") + " and effects audio is " + PlayerPrefs.GetInt("EffectsAudio"));
	}
}
