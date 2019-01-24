using UnityEngine;
using UnityEngine.UI;
using System;

public class CooldownSquare : HideableUI
{
    [SerializeField]
    public Color32 SquareColor {protected get; set;}

    void Start()
    {
        Hide();
        SquareColor = GetComponent<Image>().color;
    }

    public void UpdateSquare(int cooldown, int cooldownRemaining)
    {
        Color32 color = SquareColor;
        int alpha = 0;
        if (cooldown != 0)
        {
            alpha = 180 * (cooldownRemaining / cooldown);
        }
        color.a = Convert.ToByte(alpha);
        SquareColor = color;
        Show();
         
    }
}