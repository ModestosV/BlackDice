﻿using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
	public void PlayGame()
	{
		SceneManager.LoadScene("Game");
	}

	public void ExitGame()
	{
		Application.Quit();
	}
}
