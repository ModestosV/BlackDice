﻿using UnityEngine;
using UnityEngine.UI;

public sealed class CharacterTile : BlackDiceMonoBehaviour, IEventSubscriber
{
    private RawImage characterIcon;
    private Image border;
    private GameObject activeIndicator;
    private Animator activeAnimator;
    private GameObject deadIndicator;
    private ICharacterController character;

    private void Awake()
    {
        characterIcon = GetComponentInChildren<RawImage>();
        border = GetComponent<Image>();

        activeIndicator = Instantiate(Resources.Load("Prefabs/HUD/ActiveIndicator"), this.transform) as GameObject;
        activeIndicator.transform.SetSiblingIndex(0);
        activeAnimator = activeIndicator.GetComponent<Animator>();

        deadIndicator = Instantiate(Resources.Load("Prefabs/HUD/DeadIndicator"), this.transform) as GameObject;
        deadIndicator.SetActive(false);
    }

    public void Setup(ICharacterController character)
    {
        this.character = character;
        characterIcon.texture = character.CharacterIcon;
        border.color = character.BorderColor;
    }

    private void ShowActive()
    {
        activeAnimator.SetBool("Active", true);
    }

    private void HideActive()
    {
        activeAnimator.SetBool("Active", false);
    }

    private void ShowDead()
    {
        deadIndicator.SetActive(true);
    }

    public void Handle(IEvent @event)
    {
        var type = @event.GetType();
        if (type == typeof(DeathEvent))
        {
            var deathEvent = (DeathEvent) @event;
            if (deathEvent.CharacterController == this.character)
            {
                ShowDead();
            }
        }
        else if (type == typeof(ActiveCharacterEvent))
        {
            var activeCharacterEvent = (ActiveCharacterEvent) @event;
            if (activeCharacterEvent.CharacterController == this.character)
            {
                ShowActive();
            }
            else
            {
                HideActive();
            }
        }
    }
}
