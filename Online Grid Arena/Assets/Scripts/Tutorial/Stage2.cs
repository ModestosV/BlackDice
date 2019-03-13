using UnityEngine;
using UnityEngine.SceneManagement;

public class Stage2 : HideableUI
{
    void Start()
    {
        Show();
    }

    public void CompleteStage2()
    {
        EventBus.Publish<StageCompletedEvent>(new StageCompletedEvent(2));

        ExitToMainMenu();
    }

    public void ExitToMainMenu()
    {
        SceneManager.LoadScene(2);
    }
}