using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//defines a hexagon by setting its side length and using that to descrbie the vectors creating it
public static class Hexagon
{
    public const float outerRadius = 10f;
    public const float innerRadius = outerRadius * 0.866025404f;

    public static Vector3[] edges =
    {
        new Vector3(0f, 0f, outerRadius),
        new Vector3(innerRadius, 0f, 0.5f * outerRadius),
        new Vector3(innerRadius, 0f, -0.5f * outerRadius),
        new Vector3(0f, 0f, -outerRadius),
        new Vector3(-innerRadius, 0f, -0.5f * outerRadius),
        new Vector3(-innerRadius, 0f, 0.5f * outerRadius),
        new Vector3(0f, 0f, outerRadius)
        //this line must be used so the loop making 6 triangles loops back to the first edge rather than overflowing
    };
}