using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuButton : MonoBehaviour
{
    private Button Button { get; set; }

    void OnValidate()
    {
        Button = GetComponent<Button>();
    }

    void Awake()
    {
        Button = GetComponent<Button>();
        Button.onClick.AddListener(LoadMainMenu);
	}
	
	public void LoadMainMenu()
    {
        AudioSource music = FindObjectOfType<AudioSource>();
        if (music != null)
            Destroy(music);

        SceneManager.LoadScene(0);
    }
}
