using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class HexGrid : MonoBehaviour {

    //defines the grid size (for square)
    public int width = 6;
    public int height = 6;

    public HexObject cellPrefab;

    HexObject[] cells;

    //set to show the ID number of the cell, need both canvas and a text game object
    public Text cellLabelPrefab;
    Canvas gridCanvas;

    HexMesh hexMesh;

    void Awake()
    {
        //hexgrid receives the mesh the same way as canvas, so must be used again
        gridCanvas = GetComponentInChildren<Canvas>();
        hexMesh = GetComponentInChildren<HexMesh>();

        //re-instantiate gridCanvas
        gridCanvas = GetComponentInChildren<Canvas>();

        cells = new HexObject[height * width];

        //creates the square grid using the prefab and the set size of the grid
        for (int z = 0, i = 0; z < height; z++)
        {
            for (int x = 0; x < width; x++)
            {
                CreateCell(x, z, i++);
            }
        }
    }

    void Start()
    {
        //makes the hexgrid's mesh out of triangles
        hexMesh.TriangleHex(cells);
    }

    void CreateCell(int x, int z, int i)
    {
        //positions the cell object
        Vector3 position;
        //position.x = x * 10f;
        position.x = (x + z * 0.5f - z / 2) * (Hexagon.innerRadius * 2f);
        position.y = 0.0f;
        //position.z = z * 10f;
        position.z = z * (Hexagon.outerRadius * 1.5f);

        //stores the cells in an array, can be used later
        HexObject cell = cells[i] = Instantiate<HexObject>(cellPrefab);
        cell.transform.SetParent(transform, false); //keep local orientation rather than global
        cell.transform.localPosition = position; //set where the local position should be (what to use as the local)
        cell.coordinates = HexCoordinates.UsingOffset(x, z); //changes to updated coordinates

        //prints out the text for the actual location using x and z coordinate system
        //Because the canvas extends the grid itself, it can take the x and z positions and just print them
        Text label = Instantiate<Text>(cellLabelPrefab);
        label.rectTransform.SetParent(gridCanvas.transform, false);
        label.rectTransform.anchoredPosition = new Vector2(position.x, position.z);
        label.text = cell.coordinates.ToStringOnSeparateLines();
    }
}
