using System.Collections.Generic;

public interface ITurnPanelController
{
    void UpdateQueue(ICharacterController ActiveCharacter, List<ICharacterController> RefreshedCharacters, List<ICharacterController> ExhaustedCharacters);
}
