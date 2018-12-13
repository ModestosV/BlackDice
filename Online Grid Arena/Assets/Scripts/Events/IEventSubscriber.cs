public interface IEventSubscriber
{
    void Handle(IEvent @event);
}
