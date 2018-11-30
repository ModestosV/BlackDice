using UnityEngine;

public class LoadingCircle : MonoBehaviour
{
    private RectTransform RectComponent { get; set; }
    private const float ROTATE_SPEED = -200f;

    void Start()
    {
        RectComponent = GetComponent<RectTransform>();
    }

    void Update()
    {
        RectComponent.Rotate(0f, 0f, ROTATE_SPEED * Time.deltaTime);
    }
}
