using System.Collections;
using System.Collections.Generic;
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
        Debug.Log("AAAAAAAAAAAAHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHH");
        Debug.Log(parent.localScale.x);
        Debug.Log(parent.localScale.y);
        Debug.Log(parent.localScale.z);
        Debug.Log("NIOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOO");
        scaleX = 0.19f / (parent.localScale.x);
        scaleY = 0.19f / (parent.localScale.y);
        scaleZ = 1 / (parent.localScale.z);
    }

    private void Update()
    {
        transform.localScale = new Vector3(scaleX, scaleY, scaleZ);
    }
}
