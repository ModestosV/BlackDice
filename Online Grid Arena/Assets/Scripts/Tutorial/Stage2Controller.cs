using UnityEngine.SceneManagement;

public class Stage2Controller: IEventSubscriber
{
    private ICharacterController character;

    public Stage2Controller(ICharacterController character)
    {
        this.character = character;

        EventBus.Subscribe<StartNewTurnEvent>(this);
    }

    public void Handle(IEvent @event)
    {
        var type = @event.GetType();

        if (type == typeof(StartNewTurnEvent))
        {
            EventBus.Publish(new StageCompletedEvent(2));
            SceneManager.LoadScene(2);
        }
    }
}