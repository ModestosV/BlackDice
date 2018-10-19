using UnityEngine;

public class HexTile : MonoBehaviour, IHexTile, IHexTileSelectionController
{
    public HexTileController controller;
    public HexTileDefinition materials;

    private void OnValidate()
    {
        controller.IsEnabled = GetComponent<Renderer>().enabled;
    }

    private void Start()
    {
        controller.HexTileSelectionController = this;
        controller.GridSelectionController = GetComponentInParent<Grid>().Controller;
        controller.HexTile = this;

        GetComponent<Renderer>().material = materials.DefaultMaterial;
    }

    private void OnMouseEnter()
    {
        controller.HoverPathfinding();
    }

    private void OnMouseExit()
    {
        controller.Blur();
    }

    private void OnMouseDown()
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            controller.MultiSelect();
        } else
        {
            controller.Select();
        }
    }

    #region ISelectionController implementation

    public void Hover()
    {
        GetComponent<Renderer>().material = materials.HoveredMaterial;
    }

    public void Blur()
    {
        GetComponent<Renderer>().material = materials.DefaultMaterial;
    }

    public void Select()
    {
        GetComponent<Renderer>().material = materials.ClickedMaterial;
    }

    public void Deselect()
    {
        GetComponent<Renderer>().material = materials.DefaultMaterial;
    }

    #endregion

    public override string ToString()
    {
        return $"HexTile|x: {controller.X}, y: {controller.Y}, z: {controller.Z}";
    }

    public string Key()
    {
        return $"{controller.X}, {controller.Y}, {controller.Z}";
    }

    public HexTileController Controller()
    {
        return controller;
    }
}

