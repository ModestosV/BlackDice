using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuButton : MonoBehaviour {

    public Button mainMenuButton;

	void Start () {
        mainMenuButton.onClick.AddListener(LoadMainMenu);
	}
	
	public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
