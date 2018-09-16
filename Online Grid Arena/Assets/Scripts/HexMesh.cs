using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//class needed to render out the hexagons
[RequireComponent (typeof(MeshFilter), typeof(MeshRenderer))]
public class HexMesh : MonoBehaviour {

    //lists are used so vertices are not predetermined and can be altered easily
    Mesh hexMesh;
    List<Vector3> vertices;
    List<int> triangles;

    void Awake()
    {
        GetComponent<MeshFilter>().mesh = hexMesh = new Mesh();
        hexMesh.name = "Hex Mesh";
        vertices = new List<Vector3>();
        triangles = new List<int>();
    }

    //loops through cells and locates them all individually
    public void TriangleHex(HexObject[] cells)
    {
        hexMesh.Clear();
        vertices.Clear();
        triangles.Clear();
        for (int i = 0; i < cells.Length; i++)
        {
            TriangleHex(cells[i]);
        }
        hexMesh.vertices = vertices.ToArray();
        hexMesh.triangles = triangles.ToArray();
        hexMesh.RecalculateNormals();
    }

    //turns the hexagons into 6 triangles each before looping them into 6
    void TriangleHex(HexObject cell)
    {
        Vector3 center = cell.transform.localPosition;
        for (int i = 0; i < 6; i++)
        {
            AddTriangle(
                center,
                center + Hexagon.edges[i],
                center + Hexagon.edges[i+1]
            );
        }
    }

    //turns the hexgons into triangles given 3 vertex positions
    void AddTriangle(Vector3 v1, Vector3 v2, Vector3 v3)
    {
        int vertexIndex = vertices.Count;
        vertices.Add(v1);
        vertices.Add(v2);
        vertices.Add(v3);
        triangles.Add(vertexIndex);
        triangles.Add(vertexIndex + 1);
        triangles.Add(vertexIndex + 2);
    }

}
