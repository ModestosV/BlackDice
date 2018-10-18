using UnityEngine;

public class HexTile : MonoBehaviour, IHexTileSelectionController {

    public HexTileController controller;
    public HexTileDefinition materials;

    private void OnValidate()
    {
        controller.isEnabled = GetComponent<Renderer>().enabled;
    }

    private void Start()
    {
        controller.SetHexTileSelectionController(this);
        controller.SetGridSelectionController(GetComponentInParent<Grid>().Controller);

        GetComponent<Renderer>().material = materials.DefaultMaterial;
    }

    private void OnMouseEnter()
    {
        controller.OnMouseEnter();
    }

    private void OnMouseExit()
    {
        controller.OnMouseExit();
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
        return $"HexTile|x: {controller.x}, y: {controller.y}, z: {controller.z}";
    }

    public string Key()
    {
        return $"{controller.x}, {controller.y}, {controller.z}";
    }
}

