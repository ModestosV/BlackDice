using UnityEngine;
using System.Collections.Generic;

public class SelectionController : MonoBehaviour, IMonoBehaviour, ICharacterSelectionController, ICharacterMovementController
{
    [SerializeField] public StatPanel statPanel;

    #region ICharacterSelectionController implementation

    public ICharacter SelectedCharacter { get; set; }
    public IGridSelectionController GridSelectionController { get; set; }

    #endregion

    public IGridTraversalController GridTraversalController { get; set; }

    private void Start()
    {
        GridSelectionController = FindObjectOfType<Grid>().controller.GridSelectionController;
        GridTraversalController = FindObjectOfType<Grid>().controller.GridTraversalController;
    }

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        
        bool mouseIsOverGrid = Physics.Raycast(ray, out hit) && hit.collider.gameObject.tag == "Tile";
        bool isLeftClickDown = Input.GetMouseButtonDown(0);

        if (Input.GetKeyDown(KeyCode.Escape)) // Pressed escape to quit
        {
            Application.Quit();
        }

        if (!mouseIsOverGrid && isLeftClickDown) // Clicked off grid
        {
            GridSelectionController.ScrubPathAll();
            GridSelectionController.DeselectAll();
            statPanel.gameObject.SetActive(false);
            return;
        }

        if (!mouseIsOverGrid) // Hovered off grid
        {
            GridSelectionController.ScrubPathAll();
            return;
        }

        // Invariant: Mouse is over grid

        IHexTile targetTile = hit.collider.gameObject.GetComponent<HexTile>();
        bool tileIsEnabled = targetTile.Controller().IsEnabled;

        if (!tileIsEnabled && isLeftClickDown) // Clicked on disabled tile
        {
            GridSelectionController.ScrubPathAll();
            GridSelectionController.DeselectAll();
            statPanel.gameObject.SetActive(false);
            return;
        }

        if (!tileIsEnabled) // Hovered over disabled tile
        {
            GridSelectionController.ScrubPathAll();
            return;
        }

        // Invariant: Target tile is enabled

        bool characterIsSelected = SelectedCharacter != null;
        bool isOccupied = targetTile.Controller().OccupantCharacter != null;
        bool isCurrentSelectedTile = GridSelectionController.SelectedTiles.Count > 0 && GridSelectionController.SelectedTiles[0] == targetTile;

        if (isLeftClickDown && !characterIsSelected && !isOccupied && !isCurrentSelectedTile) // Clicked unoccupied other tile w/o character selected
        {
            GridSelectionController.BlurAll();
            targetTile.Controller().Select();
            return;
        }

        if (isLeftClickDown && !characterIsSelected && !isOccupied) // Clicked unoccupied selected tile w/o character selected
        {
            GridSelectionController.BlurAll();
            targetTile.Controller().Deselect();
            return;
        }

        if (isLeftClickDown && !characterIsSelected) // Clicked occupied tile w/o character selected
        {
            GridSelectionController.BlurAll();
            targetTile.Controller().Select();
            statPanel.gameObject.SetActive(true);
            statPanel.SetStats(SelectedCharacter.Controller().health, SelectedCharacter.Controller().damage);
            statPanel.UpdateStatValues();
            return;
        }

        if (!characterIsSelected) // Hovered over tile w/o character selected
        {
            GridSelectionController.BlurAll();
            targetTile.Controller().Hover();
            return;
        }

        // Invariant: Character is selected

        List<IHexTile> path = GridTraversalController.GetPath(SelectedCharacter.GetOccupiedTile(), targetTile);
        bool isReachable = path.Count > 0;

        if (isLeftClickDown && !isCurrentSelectedTile && !isReachable) // Clicked on unreachable tile
        {
            GridSelectionController.ScrubPathAll();
            targetTile.Controller().HoverError();
            return;
        }

        if (isLeftClickDown && !isCurrentSelectedTile && !isOccupied) // Clicked reachable unoccupied tile
        {
            GridSelectionController.ScrubPathAll();
            MoveCharacter(SelectedCharacter, targetTile);
            return;
        }

        if (isLeftClickDown && !isCurrentSelectedTile) // Clicked reachable occupied tile
        {
            GridSelectionController.ScrubPathAll();
            targetTile.Controller().HoverError();
            return;
        }

        if (isLeftClickDown && isCurrentSelectedTile) // Clicked current selected tile
        {
            GridSelectionController.ScrubPathAll();
            targetTile.Controller().Deselect();
            statPanel.gameObject.SetActive(false);
            return;
        }

        // Invariant: Left mouse button not clicked        

        if (!isReachable) // Hovered over unreachable tile
        {
            GridSelectionController.ScrubPathAll();
            GridSelectionController.BlurAll();
            targetTile.Controller().HoverError();
            return;
        }

        if (!isOccupied) // Hovered over reachable unoccupied tile
        {
            GridSelectionController.ScrubPathAll();
            GridSelectionController.BlurAll();
            GridSelectionController.HighlightPath(path);
            return;
        }

        // Hovered over reachable occupied tile
        GridSelectionController.ScrubPathAll();
        GridSelectionController.BlurAll();
        targetTile.Controller().HoverError();
        return;

    }

    #region ICharacterMovementController implementation

    public void MoveCharacter(ICharacter character, IHexTile endTile)
    {
        character.GetOccupiedTile().Controller().Deselect();
        character.GetOccupiedTile().Controller().OccupantCharacter = null;

        endTile.SetChild(character.GameObject);
        character.GameObject.transform.localPosition = new Vector3(0, 0, 0);

        endTile.Controller().OccupantCharacter = character;
        endTile.Controller().Select();
    }

    #endregion

    #region IMonoBehaviour implementation

    public GameObject GameObject
    {
        get { return gameObject; }
    }

    #endregion
}
