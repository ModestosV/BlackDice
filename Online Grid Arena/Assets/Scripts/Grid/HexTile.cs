using UnityEngine;
using System.Collections.Generic;

public sealed class HexTile : BlackDiceMonoBehaviour, IHexTile
{
    [SerializeField] private HexTileMaterialSet materials;

    protected GameObject invalidTile;
    protected GameObject damagedTile;

    public GameObject InvalidIndicator { get; set; }

    public GameObject Obstruction { get; set; }

    private HexTileController hexTileController;

    private void Awake()
    {
        Obstruction = materials.Obstruction;

        invalidTile = Instantiate(Resources.Load<GameObject>("Prefabs/HUD/Red_X"), this.transform);
        Vector3 translation = (Camera.main.transform.position - invalidTile.transform.position);
        translation.y *= 1.2f;
        invalidTile.transform.position += translation.normalized * 8.0f;
        invalidTile.SetActive(false);

        damagedTile = Instantiate(Resources.Load<GameObject>("Prefabs/HUD/DamagedTile"), this.transform);
        damagedTile.SetActive(false);

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

    }

    public void SetDefaultMaterial()
    {
        GetComponent<Renderer>().material = materials.DefaultMaterial;
        ClearTargetIndicators();
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
        invalidTile.SetActive(true);
    }

    public void ShowDamagedTarget()
    {
        damagedTile.SetActive(true);
    }

    public void ClearTargetIndicators()
    {
        invalidTile.SetActive(false);
        damagedTile.SetActive(false);
    }
}


