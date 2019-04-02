

public class Stage4Controller : AbstractStageController, IEventSubscriber
{
    private ICharacterController rocketCat;
    private ICharacterController sheepadin;
    private SelectionMode selectionMode = SelectionMode.FREE;

    public Stage4Controller(ICharacterController rocketCat, ICharacterController sheepadin)
    {
        this.rocketCat = rocketCat;
        this.sheepadin = sheepadin;
    }

    public void Handle(IEvent @event)
    {
        var type = @event.GetType();


    }
}