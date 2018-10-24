using System.Collections.Generic;

public interface ISelectionController
{
    ICharacter SelectedCharacter { get; set; }
    IGameManager GameManager { get; set; }
    IGridSelectionController GridSelectionController { get; set; }
    IGridTraversalController GridTraversalController { get; set; }
    IStatPanel StatPanel { get; set; }
    IPlayerPanel PlayerPanel { get; set; }

    void Init(IGridTraversalController gridTraversalController, IGridSelectionController gridSelectionController);

    bool IsEscapeButtonDown { get; set; }
    bool MouseIsOverGrid { get; set; }
    bool IsLeftClickDown { get; set; }
    IHexTile TargetTile { get; set; }

    void Update();
}
