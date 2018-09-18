using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour {

	public void changeScene(string sceneName)
	{
		Application.LoadLevel(sceneName);
	}

	public void exitGame()
	{
		Application.Quit();
	}
}
