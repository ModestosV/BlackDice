using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EffectStack : HideableUI, IPointerEnterHandler, IPointerExitHandler
{
    public string Description { private get; set; }
    private int stacks;
    private AbilityTooltip tooltip;

    private Text stackIndicator;

    public void Start()
    {
        tooltip = FindObjectOfType<AbilityTooltip>();
        stackIndicator = transform.GetComponentInChildren<Text>();
        Hide();
    }

    public void UpdateStacks(int newStacks, int duration)
    {
        stacks = newStacks;
        stackIndicator.text = stacks.ToString();

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

    private void PublishBuffCheckEvent()
    {
        EventBus.Publish(new BuffCheckEvent());
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        tooltip.ShowTooltip(Description);
        Invoke(methodName: nameof(PublishBuffCheckEvent), 3);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tooltip.Hide();
    }

}
