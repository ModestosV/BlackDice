using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EffectStack : HideableUI, IPointerEnterHandler, IPointerExitHandler
{
    public string Description { get; set; }
    public int Stacks { get; set; }
    private AbilityTooltip tooltip;

    private Text stackIndicator;

    public void Start()
    {
        tooltip = FindObjectOfType<AbilityTooltip>();
        stackIndicator = transform.GetComponentInChildren<Text>();
        Hide();
    }

    public void UpdateStacks(int stacks, int duration)
    {
        Stacks = stacks;
        stackIndicator.text = Stacks.ToString();

        string durRemaining = "\n" + duration;
        if (duration == 1)
        {
            durRemaining += " turn remaining";
        }
        else
        {
            durRemaining += " turns remaining";
        }
        Description += durRemaining;

        if (stacks > 0)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    private void publishBuffCheckEvent()
    {
        EventBus.Publish(new BuffCheckEvent());
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        tooltip.ShowTooltip(Description);
        Invoke("publishBuffCheckEvent", 3);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tooltip.Hide();
    }

}
