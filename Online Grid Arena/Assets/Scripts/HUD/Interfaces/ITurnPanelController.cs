using System.Collections.Generic;

public interface ITurnPanelController
{
    void updateQueue(ICharacterController ActiveCharacter, List<ICharacterController> RefreshedCharacters, List<ICharacterController> ExhaustedCharacters);
}
