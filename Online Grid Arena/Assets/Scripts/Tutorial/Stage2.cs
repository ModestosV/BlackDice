using UnityEngine;
using UnityEngine.SceneManagement;

public class Stage2 : HideableUI
{
    void Start()
    {
        Show();
    }

    public void ExitToMainMenu()
    {

        AudioSource music = FindObjectOfType<AudioSource>();
        if (music != null)
            Destroy(music);

        SceneManager.LoadScene(0);
    }
}