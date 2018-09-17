using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] //for Unity storage (according to google)
public struct HexCoordinates
{
    //used to convert to other coordinate type
    public int X { get; private set; }

    public int Z { get; private set; }

    public HexCoordinates(int x, int z)
    {
        X = x;
        Z = z;
    }

    //creates a set of coordinates using the offset that the hexes create
    public static HexCoordinates UsingOffset(int x, int z)
    {
        return new HexCoordinates(x - z / 2, z);
    }

    //Because X and Y always add up to the same result with a constant Z, we can say the following
    public int Y
    {
        get
        {
            return -X - Z;
        }
    }

    //prints out coordinates
    public override string ToString()
    {
        return "(" + X.ToString() + ", " + Y.ToString() + ", " + Z.ToString() + ")";
    }

    public string ToStringOnSeparateLines()
    {
        return X.ToString() + "\n" + Y.ToString() + "\n" + Z.ToString();
    }
}

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