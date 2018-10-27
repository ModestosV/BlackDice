public interface IAbilitySelectionController
{
    bool IsEscapeButtonDown { get; set; }
    bool MouseIsOverGrid { get; set; }
    bool IsLeftClickDown { get; set; }
    IHexTile TargetTile { get; set; }

    IGridSelectionController GridSelectionController { get; set; }
    IGridTraversalController GridTraversalController { get; set; }
    IAbilityExecutionController AbilityExecutionController { get; set; }

    ISelectionController SelectionController { get; set; }

    void Update(IAbility selectedAbility);
}
