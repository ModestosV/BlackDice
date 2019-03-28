using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIDescriptionView : HideableUI
{
    private bool view;

    private void Start()
    {
        Hide();
    }

    public void ToggleView()
    {
        if (view)
        {
            Hide();
            view = false;
        }
        else if (!view)
        {
            Show();
            view = true;
        }
    }
}
