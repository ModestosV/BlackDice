using UnityEngine;

public class ActiveCircle : HideableUI
{
    public bool isActive;

    private SpriteRenderer spriteRenderer;
    private float flickerOffsetTime = 1.0f;
    private float currentTime = 1.0f;

    void Awake()
    {
        isActive = false;
        spriteRenderer = GetComponent<SpriteRenderer>();    
    }

    private void Update()
    {
        currentTime -= Time.deltaTime;
        if (isActive == false)
        {
            spriteRenderer.enabled = false;
        }
        else
        {
            if (currentTime <= 0.0f)
            {
                Flicker();
                currentTime = flickerOffsetTime;
            }
        }
    }

    private void Flicker()
    {
        spriteRenderer.enabled = !spriteRenderer.enabled;
    }
}