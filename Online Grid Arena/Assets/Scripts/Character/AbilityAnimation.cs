using UnityEngine;
using UnityEngine.UI;

public class AbilityAnimation : MonoBehaviour {

    protected Image image;
    protected readonly float fadeOutSpeed = 1.0f;

	void Start ()
    {
        image = GetComponentInChildren<Image>();
	}

    void Update()
    {
        image.color = new Color(image.color.r, image.color.g, image.color.b, image.color.a - fadeOutSpeed * Time.deltaTime);
        
        if (image.color.a <= 0.0f)
        {
            Destroy(gameObject);
        }
    }
}
