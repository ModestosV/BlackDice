using System;
using UnityEngine;

public class HexTile : MonoBehaviour, IHexTile
{
    // Public members to be instantiated by/through Unity
    public HexTileController controller;
    public HexTileMaterialSet materials;

    private void Awake()
    {
        controller.OccupantCharacter = GetComponentInChildren<Character>().Controller;
    }

    private void Start()
    {
        controller.IsEnabled = GetComponent<Renderer>().enabled;
        GetComponent<Renderer>().material = materials.DefaultMaterial;

        controller.HexTile = this;
    }

    #region IHexTile implementation

    public void SetHoverMaterial()
    {
        GetComponent<Renderer>().material = materials.HoveredMaterial;
    }

    public void SetErrorMaterial()
    {
        GetComponent<Renderer>().material = materials.HoveredErrorMaterial;
    }

    public void SetDefaultMaterial()
    {
        GetComponent<Renderer>().material = materials.DefaultMaterial;
    }

    public void SetClickedMaterial()
    {
        GetComponent<Renderer>().material = materials.ClickedMaterial;
    }

    public void SetHighlightMaterial()
    {
        GetComponent<Renderer>().material = materials.PathMaterial;
    }

    public bool IsMouseOver()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        return Physics.Raycast(ray, out hit) && hit.collider.gameObject.GetComponent<HexTile>() == this;
    }

    public IHexTileController Controller
    {
        get { return controller; }
    }

    public void SetChild(GameObject childObject)
    {
        childObject.transform.parent = gameObject.transform;
    }

    #endregion

    #region IMonoBehaviour implementation

    public GameObject GameObject
    {
        get { return gameObject; }
    }

    #endregion

    public override string ToString()
    {
        return $"HexTile|x: {controller.X}, y: {controller.Y}, z: {controller.Z}";
    }
}


