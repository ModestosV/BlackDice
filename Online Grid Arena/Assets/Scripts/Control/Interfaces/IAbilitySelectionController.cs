public interface IAbilitySelectionController : IInputController
{
    IGridSelectionController GridSelectionController { set; }
    IGameManager GameManager { set; }
    int ActiveAbilityNumber { set; }
    
    void Update();
}
