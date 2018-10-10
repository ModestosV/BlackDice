using UnityEngine;

public class GridMap : MonoBehaviour
{
    public GridMapController GridMapInstance;

    private void Start()
    {
        GridMapInstance?.InitializeMap();
    }
}