using UnityEngine;
using UnityEngine.UI;
using System;

public class CooldownSquare : HideableUI
{
    [SerializeField]
    public Color SquareColor {protected get; set;}
    public Text SquareText { protected get; set; }

    void Start()
    {
        Hide();
        SquareColor = GetComponent<Image>().color;
        SquareText = GetComponentInChildren<Text>();
    }

    public void UpdateSquare(int cooldown, int cooldownRemaining)
    {
        Text text = SquareText;
        Color color = SquareColor;
        int alpha = 0;
        if (cooldown != 0)
        {
            alpha = (cooldownRemaining / cooldown);
            text.text = cooldownRemaining.ToString();
        }
        color.a = Convert.ToByte(alpha);
        SquareColor = color;
        SquareText = text;
        Show(); 
    }
}