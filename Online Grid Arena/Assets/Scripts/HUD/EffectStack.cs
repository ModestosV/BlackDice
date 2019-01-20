using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EffectStack : HideableUI, IPointerEnterHandler, IPointerExitHandler
{
    public string Description;
    public int Stacks;
    public GraphicRaycaster ray;
    public AbilityToolTip tooltip;

    public Text stackIndicator;

    public void Start()
    {
        ray = this.GetComponent<GraphicRaycaster>();
        tooltip = FindObjectOfType<AbilityToolTip>();
        stackIndicator = transform.GetComponentInChildren<Text>();
        Hide();
    }

    public void Update()
    {
        if (Stacks > 0)
        {
            Show();
        }
        else
        {
            Hide();
        }
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
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        tooltip.ShowToolTip(Description);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tooltip.Hide();
    }

}
