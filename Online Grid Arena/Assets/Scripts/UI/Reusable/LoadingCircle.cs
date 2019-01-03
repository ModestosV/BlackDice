using UnityEngine;

public sealed class LoadingCircle : MonoBehaviour
{
    private RectTransform rectComponent;
    private readonly float rotateSpeed = -200f;

    void Start()
    {
        rectComponent = GetComponent<RectTransform>();
    }

    void Update()
    {
        rectComponent.Rotate(0f, 0f, rotateSpeed * Time.deltaTime);
    }
}
