﻿using UnityEngine.EventSystems;

public class PortraitAbility : BlackDiceMonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private AbilityTooltip tooltip;

    public string Description { private get; set; }

    void Start()
    {
        tooltip = FindObjectOfType<AbilityTooltip>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        tooltip.ShowTooltip(Description);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tooltip.Hide();
    }
}
