using UnityEngine;

public class ActiveCircle : HideableUI
{
    public bool isActive;

    private void Update()
    {
        enabled = isActive;
    }
}