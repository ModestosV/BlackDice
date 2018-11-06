public interface IAbilitySelectionController
{
    IGridSelectionController GridSelectionController { set; }
    IGameManager GameManager { set; }
    
    void Update();
}
