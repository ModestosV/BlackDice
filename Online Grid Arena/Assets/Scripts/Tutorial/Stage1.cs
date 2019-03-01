using UnityEngine;
using UnityEngine.SceneManagement;

public class Stage1 : HideableUI
{
    void Start()
    {
        Show();
    }

    public void CompleteStage1()
    {
        EventBus.Publish<StageCompletedEvent>(new StageCompletedEvent(1));

        ExitToMainMenu();
    }

    public void ExitToMainMenu()
    {

        AudioSource music = FindObjectOfType<AudioSource>();
        if (music != null)
            Destroy(music);

        SceneManager.LoadScene(0);
    }
}