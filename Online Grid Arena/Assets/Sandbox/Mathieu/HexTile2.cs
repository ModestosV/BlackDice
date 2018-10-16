using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexTile2 : MonoBehaviour, ISelectionController {

    public HexTileController2 Controller;
    public HexTileDefinition materials;

    private void OnValidate()
    {
        Controller.enabled = GetComponent<Renderer>();
    }

    private void Start()
    {
        Controller.Init(this);

        GetComponent<Renderer>().material = materials.DefaultMaterial;
    }

    private void OnMouseEnter()
    {
        Controller.Hover();
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
}
