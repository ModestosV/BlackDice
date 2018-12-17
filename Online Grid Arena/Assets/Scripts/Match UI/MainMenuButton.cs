using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public sealed class MainMenuButton : MonoBehaviour
{
    private Button button;

    void OnValidate()
    {
        button = GetComponent<Button>();
    }

    void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(LoadMainMenu);
	}
	
	public void LoadMainMenu()
    {
        AudioSource music = FindObjectOfType<AudioSource>();
        if (music != null)
            Destroy(music);

        SceneManager.LoadScene(0);
    }
}
