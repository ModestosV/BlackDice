using UnityEngine;

public class ScaleTiles : BlackDiceMonoBehaviour
{
    Transform parent;
    float scaleX;
    float scaleY;
    float scaleZ;

    private void Awake()
    {
        parent = transform.parent;
        var localScale = parent.localScale;
        Debug.Log(localScale.x);
        Debug.Log(localScale.y);
        Debug.Log(localScale.z);
        scaleX = 0.19f / (localScale.x);
        scaleY = 0.19f / (localScale.y);
        scaleZ = 1 / (localScale.z);
    }

    private void Update()
    {
        transform.localScale = new Vector3(scaleX, scaleY, scaleZ);
    }
}
