using UnityEngine;
using UnityEngine.UI;

public sealed class CharacterTile : BlackDiceMonoBehaviour
{
    private RawImage characterIcon;
    private Image border;
    private GameObject activeIndicator;
    private Animator activeAnimator;

    [SerializeField]
    public GameObject ActiveIndicator;

    private void Awake()
    {
        characterIcon = GetComponentInChildren<RawImage>();
        border = GetComponent<Image>();

        activeIndicator = Instantiate(ActiveIndicator, this.transform) as GameObject;
        activeIndicator.transform.SetSiblingIndex(0);
        activeAnimator = activeIndicator.GetComponent<Animator>();
    }

    public void Setup(Texture texture, Color32 color32)
    {
        characterIcon.texture = texture;
        border.color = color32;
    }

    public void ShowActive()
    {
        activeAnimator.SetBool("Active", true);
    }

    public void HideActive()
    {
        activeAnimator.SetBool("Active", false);
    }
}
