﻿using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : HideableUI
{
    void Start()
    {
        Show();
    }

    public void PlayGame()
	{
		SceneManager.LoadScene(1);
	}

	public void ExitGame()
	{
		Application.Quit();
	}
}