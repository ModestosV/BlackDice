using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class HexTile2 : MonoBehaviour, ISelectionController {

    public HexTileController2 Controller;
    public HexTileDefinition materials;

    private void OnValidate()
    {
        Controller.enabled = GetComponent<Renderer>().enabled;
    }

    private void Start()
    {
        Controller.Init(this);

        GetComponent<Renderer>().material = materials.DefaultMaterial;
    }

    private void OnMouseEnter()
    {
        Controller.Hover();

        TestPath();
    }

    private void OnMouseExit()
    {
        Controller.Blur();
    }

    private void OnMouseDown()
    {
        if (!Input.GetKey(KeyCode.LeftControl))
            GetComponentInParent<Grid>().Controller.DeselectAll();
        Controller.Select();

        //TestSelect();
    }

    #region ISelectionController implementation

    public void Hover()
    {
        GetComponent<Renderer>().material = materials.HoveredMaterial;
        GetComponentInParent<Grid>().Controller.HoveredTiles.Add(this);
    }
    public void Blur()
    {
        GetComponent<Renderer>().material = materials.DefaultMaterial;
        GetComponentInParent<Grid>().Controller.HoveredTiles.Remove(this);
    }

    private void TestSelect()
    {
        var neighbors = GetComponentInParent<Grid>().Controller.getNeighbors(this);
        foreach (HexTile2 neighbor in neighbors)
        {
            neighbor.Controller.Select();
        }
    }

    private void TestPath()
    {
        var selectedTiles = GetComponentInParent<Grid>().Controller.SelectedTiles;
        if (selectedTiles.Count > 0)
        {
            GetComponentInParent<Grid>().Controller.BlurAll();


            var path = GetComponentInParent<Grid>().Controller.getPath(selectedTiles.ElementAt(0), this);

            foreach (var tile in path)
            {
                tile.Hover();
            }

        }
    }

    public void Select()
    {
        GetComponent<Renderer>().material = materials.ClickedMaterial;
        GetComponentInParent<Grid>().Controller.SelectedTiles.Add(this);
    }

    public void Deselect()
    {
        GetComponent<Renderer>().material = materials.DefaultMaterial;
        GetComponentInParent<Grid>().Controller.SelectedTiles.Remove(this);
    }

    #endregion

    public override string ToString()
    {
        return $"HexTile|x: {Controller.x}, y: {Controller.y}, z: {Controller.z}";
    }
}
