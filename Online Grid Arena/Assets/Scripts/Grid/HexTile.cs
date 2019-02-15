using UnityEngine;
using System.Collections.Generic;

public sealed class HexTile : BlackDiceMonoBehaviour, IHexTile
{
    [SerializeField] private HexTileMaterialSet materials;
    [SerializeField] private GameObject invalidTile;

    public GameObject TargetIndicator { get; set; }

    public GameObject Obstruction { get; set; }

    private HexTileController hexTileController;

    private void Awake()
    {
        Obstruction = materials.Obstruction;

        hexTileController = new HexTileController()
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
    public void SetHoverMaterial()
    {
        GetComponent<Renderer>().material = materials.HoveredMaterial;
    }

    public void SetErrorMaterial()
    {
        //GetComponent<Renderer>().material = materials.HoveredErrorMaterial;
        TargetIndicator = Instantiate(invalidTile, gameObject.transform) as GameObject;
        TargetIndicator.transform.LookAt(Camera.main.transform.position, -Vector3.up);
    }

    public void SetDefaultMaterial()
    {
        GetComponent<Renderer>().material = materials.DefaultMaterial;
        ClearTargetIndicator();
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

    public void ShowInvalidTarget()
    {
        // Show Red X
        TargetIndicator = Instantiate(invalidTile, gameObject.transform) as GameObject;

        // Position X relative to camera
        Vector3 translation = (Camera.main.transform.position - TargetIndicator.transform.position);
        translation.y *= 1.2f;
        TargetIndicator.transform.position += translation.normalized * 8.0f;


        TargetIndicator.transform.rotation = Quaternion.Euler(90, 0, 0);
    }

    public void ClearTargetIndicator()
    {
        Destroy(TargetIndicator);
    }
}


