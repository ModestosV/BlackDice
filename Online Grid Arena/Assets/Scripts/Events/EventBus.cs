using System;
using System.Collections.Generic;

public static class EventBus
{
    private static Dictionary<Type, List<IEventSubscriber>> registry = new Dictionary<Type, List<IEventSubscriber>>();

    public static void Subscribe<T>(IEventSubscriber eventSubsciber) where T : IEvent
    {
        if (!registry.ContainsKey(typeof(T)))
        {
            registry[typeof(T)] = new List<IEventSubscriber>();
        }

        registry[typeof(T)].Add(eventSubsciber);
    }

    public static void Publish<T>(T @event) where T : IEvent
    {
        if (registry.ContainsKey(typeof(T)))
        {
            foreach (dynamic subscriber in registry[typeof(T)])
            {
                subscriber.Handle(@event);
            }
        }
    }

    public static void Reset()
    {
        registry = new Dictionary<Type, List<IEventSubscriber>>();
    }
}