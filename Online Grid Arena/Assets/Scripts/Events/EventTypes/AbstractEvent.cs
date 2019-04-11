using UnityEngine;

public abstract class AbstractEvent : IEvent
{
    protected AbstractEvent()
    {
        Debug.Log(ToString() + " fired");
    }
}