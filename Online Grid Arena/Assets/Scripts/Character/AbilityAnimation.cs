using UnityEngine;
using UnityEngine.UI;

public class AbilityAnimation : MonoBehaviour {

    private Image Image { get; set; }

    private const float FADE_OUT_SPEED = 1.0f;

	void Start ()
    {
        Image = GetComponentInChildren<Image>();
	}

    void Update()
    {
        Image.color = new Color(Image.color.r, Image.color.g, Image.color.b, Image.color.a - FADE_OUT_SPEED*Time.deltaTime);
        
        if (Image.color.a <= 0.0f)
        {
            Destroy(gameObject);
        }
    }
}
