using System.Collections.Generic;

public interface ITurnController
{
    List<ICharacterController> RefreshedCharacters { set; }
    List<ICharacterController> ExhaustedCharacters { set; }
    ICharacterController ActiveCharacter { set; }
    ITurnPanelController TurnTracker { set; }
    ISelectionManager SelectionManager { set; }
    
    void AddCharacters(List<ICharacterController> characters);
    void AddCharacter(ICharacterController character);
    void SelectActiveCharacter();
    List<ICharacterController> GetLivingCharacters();
    bool IsActiveCharacter(ICharacterController character);
}
