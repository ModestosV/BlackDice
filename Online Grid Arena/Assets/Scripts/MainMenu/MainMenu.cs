using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using System.Collections;
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
