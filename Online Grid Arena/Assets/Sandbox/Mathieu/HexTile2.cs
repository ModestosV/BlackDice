using UnityEngine;

public class HexTile2 : MonoBehaviour, ISelectionController2 {

    public HexTileController2 controller;
    public HexTileDefinition materials;

    private void OnValidate()
    {
        controller.isEnabled = GetComponent<Renderer>().enabled;
    }

    private void Start()
    {
        controller.Init(this);

        GetComponent<Renderer>().material = materials.DefaultMaterial;
    }

    private void OnMouseEnter()
    {
        GetComponentInParent<Grid>().Controller.BlurAll();
        controller.Hover();
        DrawPath();
    }

    private void OnMouseExit()
    {
        controller.Blur();
    }

    private void OnMouseDown()
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            controller.Select();
        } else
        {
            GetComponentInParent<Grid>().Controller.BlurAll();
            if (!controller.isSelected)
            {
                GetComponentInParent<Grid>().Controller.DeselectAll();
                controller.Select();
            } else
            {
                controller.Deselect();
            }
        }

        //TestSelect();
    }

    #region ISelectionController implementation

    public void Hover()
    {
        GetComponent<Renderer>().material = materials.HoveredMaterial;
        GetComponentInParent<Grid>().Controller.hoveredTiles.Add(this);
    }
    public void Blur()
    {
        GetComponent<Renderer>().material = materials.DefaultMaterial;
        GetComponentInParent<Grid>().Controller.hoveredTiles.Remove(this);
    }

    private void TestSelect()
    {
        var neighbors = GetComponentInParent<Grid>().Controller.getNeighbors(this);
        foreach (HexTile2 neighbor in neighbors)
        {
            neighbor.controller.Select();
        }
    }

    private void DrawPath()
    {
        GetComponentInParent<Grid>().Controller.BlurAll();

        var selectedTiles = GetComponentInParent<Grid>().Controller.selectedTiles;

        if (selectedTiles.Count > 0)
        {
            foreach (HexTile2 startTile in selectedTiles)
            {
                var path = GetComponentInParent<Grid>().Controller.getPath(startTile, this);

                foreach (var tile in path)
                {
                    tile.controller.Hover();
                }
            }
        }
    }

    public void Select()
    {
        GetComponent<Renderer>().material = materials.ClickedMaterial;
        GetComponentInParent<Grid>().Controller.selectedTiles.Add(this);
    }

    public void Deselect()
    {
        GetComponent<Renderer>().material = materials.DefaultMaterial;
        GetComponentInParent<Grid>().Controller.selectedTiles.Remove(this);
    }

    #endregion

    public override string ToString()
    {
        return $"HexTile|x: {controller.x}, y: {controller.y}, z: {controller.z}";
    }

    public string Key()
    {
        return $"{controller.x}, {controller.y}, {controller.z}";
    }
}

