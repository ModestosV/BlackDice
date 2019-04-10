using UnityEngine;
using UnityEngine.UI;

public class AbilityAnimation : MonoBehaviour
{
    private Image image;
    private readonly float fadeOutSpeed = 1.0f;

	void Start()
    {
        image = GetComponentInChildren<Image>();

        Camera camera = Camera.main;
        var rotation = camera.transform.rotation;
        var position = transform.position;

        transform.LookAt(position + rotation * Vector3.forward, rotation * Vector3.up);
        transform.position = Vector3.MoveTowards(position, camera.transform.position, 1.5f);
    }

    void Update()
    {
        var color = image.color;
        image.color = new Color(color.r, color.g, color.b, color.a - fadeOutSpeed * Time.deltaTime);

        if (image.color.a <= 0.0f)
        {
            Destroy(gameObject);
        }
    }
}
