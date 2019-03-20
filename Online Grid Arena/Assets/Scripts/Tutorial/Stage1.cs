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
        SceneManager.LoadScene(2);
    }
}