public interface ICharacterSelectionController
{
    IGridSelectionController GridSelectionController { get; set; }
    ICharacter SelectedCharacter { get; set; }
}
