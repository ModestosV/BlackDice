﻿using UnityEngine;

public class MatchMenu : HideableUI, IMatchMenu, IEventSubscriber
{
    private bool visible;
    private CanvasGroup canvasGroup;

    void OnValidate()
    {
        Init();
    }

    void Awake()
    {
        Init();
        Hide();
    }

    public void Toggle()
    {
        visible = !visible;

        if (!visible)
        {
            Hide();
        } else
        {
            Show();
        }
    }

    public void Handle(IEvent @event)
    {
        var type = @event.GetType();
        if (type == typeof(SurrenderEvent))
        {
            Toggle();
        }
    }
}
