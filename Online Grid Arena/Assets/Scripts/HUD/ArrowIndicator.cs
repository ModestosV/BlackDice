using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowIndicator : HideableUI
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(ToString() + "Hiding");
        Hide();
    }

    void ShowArrow()
    {
        Debug.Log(ToString() + "Showing");
        Show();
    }
}
