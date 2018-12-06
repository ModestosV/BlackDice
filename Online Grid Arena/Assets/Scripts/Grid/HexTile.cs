﻿using UnityEngine;

public sealed class HexTile : MonoBehaviour, IHexTile
{
<<<<<<< HEAD
    [SerializeField] private HexTileMaterialSet materials;
=======
    public HexTileMaterialSet materials;
<<<<<<< HEAD
    private Material Obstruction;
>>>>>>> #15 Add in Hex Tile Material Set a Obstruction Material
=======
    private GameObject Obstruction;
>>>>>>> #15 Make path NOT go through obstructed and occupied tiles

    private HexTileController hexTileController;

    private void Awake()
    {
        Obstruction = materials.Obstruction;

        hexTileController = new HexTileController
        {
            HexTile = this,
            IsEnabled = GetComponent<Renderer>().enabled,
            IsObstructed = GetObstruction() != null
        };
        GetComponent<Renderer>().material = materials.DefaultMaterial;

    }

    private void Start()
    {
        LinkOccupiedCharacter();
    }

    private void LinkOccupiedCharacter()
    {
        ICharacter occupantCharacter = GetComponentInChildren<AbstractCharacter>();

        if (occupantCharacter == null) return;

        occupantCharacter.Controller.OccupiedTile = hexTileController;
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

    public GameObject GetObstruction()
    {
        return Obstruction;
    }

    public bool IsMouseOver()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        return Physics.Raycast(ray, out hit) && hit.collider.gameObject.GetComponent<HexTile>() == this;
    }

    public IHexTileController Controller
    {
        get { return hexTileController; }
    }
    
    public void PlayAbilityAnimation(GameObject abilityAnimationPrefab)
    {
        Instantiate(abilityAnimationPrefab, gameObject.transform);
    }

    #endregion

    #region IMonoBehaviour implementation

    public GameObject GameObject
    {
        get { return gameObject; }
    }

    #endregion

}


