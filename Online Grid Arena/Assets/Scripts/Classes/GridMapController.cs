using UnityEngine;

public class GridMapController : MonoBehaviour
{
    public GridMap GridMap;

    private void Start()
    {
        GridMap?.InitializeMap();
    }
}