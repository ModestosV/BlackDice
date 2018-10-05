using UnityEngine;

public class GridMapRenderer : MonoBehaviour
{
    public GridMap GridMap;

    private void Start()
    {
        GridMap?.InitializeMap();
    }
}