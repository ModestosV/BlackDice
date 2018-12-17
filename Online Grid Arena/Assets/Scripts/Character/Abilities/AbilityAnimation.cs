using UnityEngine;
using UnityEngine.UI;

public class AbilityAnimation : MonoBehaviour
{
    protected Image image;
    protected readonly float fadeOutSpeed = 1.0f;

	void Start()
    {
        image = GetComponentInChildren<Image>();

        Camera camera = Camera.main;
        transform.LookAt(transform.position + camera.transform.rotation * Vector3.forward, camera.transform.rotation * Vector3.up);
        transform.position = Vector3.MoveTowards(transform.position, camera.transform.position, 1.5f);
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
