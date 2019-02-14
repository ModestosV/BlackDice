using UnityEngine;

public abstract class AbstractEvent : IEvent
{
    public AbstractEvent()
    {
        Debug.Log(ToString() + " fired");
    }
}