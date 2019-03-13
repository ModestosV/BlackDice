using UnityEngine.SceneManagement;

public class Stage2Controller: IStageController
{
    private ICharacterController character;

    public Stage2Controller(ICharacterController character)
    {
        this.character = character;

    }

    public void EndStage()
    {
        EventBus.Publish(new StageCompletedEvent(2));
        SceneManager.LoadScene(2);
    }
}