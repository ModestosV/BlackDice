using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public sealed class CharacterTile : BlackDiceMonoBehaviour, IEventSubscriber, IPointerClickHandler
{
    private RawImage characterIcon;
    private Image border;

    private GameObject activeIndicator;
    private Animator activeAnimator;
    private GameObject deadIndicator;
    private GameObject exhaustedIndicator;
    private ICharacterController character;
    private GameObject shieldIndicator;
    private GameObject stunIndicator;

    private void Awake()
    {
        characterIcon = GetComponentInChildren<RawImage>();
        border = GetComponent<Image>();

        activeIndicator = Instantiate(Resources.Load("Prefabs/HUD/ActiveIndicator"), this.transform) as GameObject;
        activeIndicator.transform.SetSiblingIndex(0);
        activeAnimator = activeIndicator.GetComponent<Animator>();

        deadIndicator = Instantiate(Resources.Load("Prefabs/HUD/DeadIndicator"), this.transform) as GameObject;
        deadIndicator.SetActive(false);

        exhaustedIndicator = Instantiate(Resources.Load("Prefabs/HUD/ExhaustedIndicator"), this.transform) as GameObject;
        exhaustedIndicator.SetActive(false);

        shieldIndicator = Instantiate(Resources.Load("Prefabs/HUD/ShieldIndicator"), this.transform) as GameObject;
        HideShield();

        stunIndicator = Instantiate(Resources.Load("Prefabs/HUD/StunIndicator"), this.transform) as GameObject;
        HideStun();
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

    private void ShowExhausted()
    {
        exhaustedIndicator.SetActive(true);
    }

    private void HideExhausted()
    {
        exhaustedIndicator.SetActive(false);
    }

    private void ShowShield()
    {
        shieldIndicator.SetActive(true);
    }

    private void HideShield()
    {
        shieldIndicator.SetActive(false);
    }

    private void ShowStun()
    {
        stunIndicator.SetActive(true);
    }

    private void HideStun()
    {
        stunIndicator.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        EventBus.Publish(new SelectTileEvent(character.OccupiedTile));
    }

    public void Handle(IEvent @event)
    {
        var type = @event.GetType();
        if (type == typeof(DeathEvent))
        {
            var deathEvent = (DeathEvent)@event;
            if (deathEvent.CharacterController == this.character)
            {
                ShowExhausted();
                ShowDead();
            }
        }
        else if (type == typeof(SelectCharacterEvent))
        {
            var activeCharacterEvent = (SelectCharacterEvent)@event;
            if (activeCharacterEvent.Character == this.character)
            {
                ShowActive();
            }
        }
        else if (type == typeof(ExhaustCharacterEvent))
        {
            var exhaustCharacterEvent = (ExhaustCharacterEvent)@event;
            if (exhaustCharacterEvent.CharacterController == this.character)
            {
                ShowExhausted();
                HideActive();
            }
        }
        else if (type == typeof(NewRoundEvent))
        {
            var newRoundEvent = (NewRoundEvent)@event;
            if (newRoundEvent.CharacterController == this.character)
            {
                HideExhausted();
            }
        }
        else if (type == typeof(StatusEffectEvent))
        {
            var statusEffectEvent = (StatusEffectEvent)@event;
            if (statusEffectEvent.CharacterController == this.character)
            {
                //shield, stun, silence
                if (statusEffectEvent.Type == "shield")
                {
                    if (statusEffectEvent.IsActive)
                    {
                        ShowShield();
                    }
                    else
                    {
                        HideShield();
                    }
                }
                if (statusEffectEvent.Type == "stun")
                {
                    if (statusEffectEvent.IsActive)
                    {
                        ShowStun();
                    }
                    else
                    {
                        HideStun();
                    }
                }
            }
        }
    }
}
