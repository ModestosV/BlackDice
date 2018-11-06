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
        AudioSource music = FindObjectOfType<AudioSource>();
        if (music != null)
            Destroy(music);

        SceneManager.LoadScene(0);
    }
}
