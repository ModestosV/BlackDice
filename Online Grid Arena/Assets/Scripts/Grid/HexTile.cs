﻿using System;
using UnityEngine;

public class HexTile : MonoBehaviour, IHexTile, IHexTileSelectionController
{
    public HexTileController controller;
    public HexTileMaterialSet materials;

    private void Awake()
    {
        controller.IsEnabled = GetComponent<Renderer>().enabled;
        GetComponent<Renderer>().material = materials.DefaultMaterial;
        controller.OccupantCharacter = GetComponentInChildren<Character>();
        controller.CharacterSelectionController = FindObjectOfType<SelectionController>();
        controller.HexTileSelectionController = this;
        controller.HexTile = this;
    }

    private void Start()
    {
        controller.GridSelectionController = GetComponentInParent<Grid>().controller.GridSelectionController;
        controller.GridTraversalController = GetComponentInParent<Grid>().controller.GridTraversalController;
    }

    private void OnMouseExit()
    {
        controller.Blur();
    }

    #region ISelectionController implementation

    public void Hover()
    {
        GetComponent<Renderer>().material = materials.HoveredMaterial;
    }

    public void HoverError()
    {
        GetComponent<Renderer>().material = materials.HoveredErrorMaterial;
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
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit) && hit.collider.gameObject.GetComponent<HexTile>() == this)
        {
            GetComponent<Renderer>().material = materials.HoveredMaterial;
        } else
        {
            GetComponent<Renderer>().material = materials.DefaultMaterial;
        }
    }

    public void MarkPath()
    {
        GetComponent<Renderer>().material = materials.PathMaterial;
    }

    public void ScrubPath()
    {
        GetComponent<Renderer>().material = materials.DefaultMaterial;
    }

    #endregion

    public override string ToString()
    {
        return $"HexTile|x: {controller.X}, y: {controller.Y}, z: {controller.Z}";
    }

    #region IHexTile implementation

    public HexTileController Controller()
    {
        return controller;
    }

    public Tuple<int, int, int> Coordinates()
    {
        return new Tuple<int, int, int>(controller.X, controller.Y, controller.Z);
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
}


