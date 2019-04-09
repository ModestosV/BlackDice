using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public sealed class CharacterTile : BlackDiceMonoBehaviour, IEventSubscriber, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    private RawImage characterIcon;
    private Image border;
    private IHealthBar healthBar;

    private GameObject activeIndicator;
    private Animator activeAnimator;
    private GameObject deadIndicator;
    private GameObject exhaustedIndicator;
    private ICharacterController character;
    private GameObject shieldIndicator;
    private GameObject stunIndicator;
    private GameObject silenceIndicator;

    private GameObject abilityPanel;

    private void Awake()
    {
        characterIcon = GetComponentInChildren<RawImage>();
        border = GetComponent<Image>();

        abilityPanel = transform.GetChild(1).gameObject;

        activeIndicator = Instantiate(Resources.Load("Prefabs/HUD/ActiveIndicator"), this.transform) as GameObject;
        activeIndicator.transform.SetSiblingIndex(0);
        activeAnimator = activeIndicator.GetComponent<Animator>();
        
        var healthBarObject = Instantiate(Resources.Load<GameObject>("Prefabs/Characters/HealthBar"), this.transform);
        healthBarObject.transform.SetParent(this.transform);
        healthBarObject.transform.localPosition -= new Vector3(0.0f, 37.8f);
        healthBarObject.transform.localScale = new Vector3(0.41f, 1.5f);
        healthBarObject.transform.SetSiblingIndex(2);

        healthBar = healthBarObject.GetComponent<HealthBar>();

        deadIndicator = Instantiate(Resources.Load("Prefabs/HUD/DeadIndicator"), this.transform) as GameObject;
        deadIndicator.SetActive(false);

        exhaustedIndicator = Instantiate(Resources.Load("Prefabs/HUD/ExhaustedIndicator"), this.transform) as GameObject;
        exhaustedIndicator.SetActive(false);

        shieldIndicator = Instantiate(Resources.Load("Prefabs/HUD/ShieldIndicator"), this.transform) as GameObject;
        HideShield();

        stunIndicator = Instantiate(Resources.Load("Prefabs/HUD/StunIndicator"), this.transform) as GameObject;
        HideStun();

        silenceIndicator = Instantiate(Resources.Load("Prefabs/HUD/SilenceIndicator"), this.transform) as GameObject;
        HideSilence();
    }

    public void Setup(ICharacterController character)
    {
        this.character = character;
        characterIcon.texture = character.CharacterIcon;
        border.color = character.BorderColor;
        InitializeAbilityPanel();
    }

    private void InitializeAbilityPanel()
    {
        abilityPanel.GetComponent<PortraitAbilityPanel>().UpdateAbilityIcons(character.Abilities);
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

    private void ShowSilence()
    {
        silenceIndicator.SetActive(true);
    }

    private void HideSilence()
    {
        silenceIndicator.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        EventBus.Publish(new SelectTileEvent(character.OccupiedTile));
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        character.UpdateTargetHUD();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        character.ClearTargetHUD();
    }

    public void UpdateHealthBar()
    {
        healthBar.SetHealthBarRatio(character.CharacterStats["health"].CurrentValue / character.CharacterStats["health"].Value);
        healthBar.SetHealthText(Mathf.CeilToInt(character.CharacterStats["health"].CurrentValue).ToString(), Mathf.CeilToInt(character.CharacterStats["health"].Value).ToString());
    }

    public void Handle(IEvent @event)
    {
        var type = @event.GetType();
        if (type == typeof(DamageEvent))
        {
            UpdateHealthBar();
        }
        else if (type == typeof(AbilityUsedEvent))
        {
            UpdateHealthBar();
        }
        else if (type == typeof(StartNewTurnEvent))
        {
            UpdateHealthBar();
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
                if (statusEffectEvent.Type == "silence")
                {
                    if (statusEffectEvent.IsActive)
                    {
                        ShowSilence();
                    }
                    else
                    {
                        HideSilence();
                    }
                }
            }
        }
        else if (type == typeof(DeathEvent))
        {
            var deathEvent = (DeathEvent)@event;
            if (deathEvent.CharacterController == this.character)
            {
                ShowExhausted();
                ShowDead();
            }
        }
    }
}
