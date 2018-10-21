public interface ICharacterMovementController
{
    IGridTraversalController GridTraversalController { get; set; }

    void MoveCharacter(ICharacter character, IHexTile endTile);
}
