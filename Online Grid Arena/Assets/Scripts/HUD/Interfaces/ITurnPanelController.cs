using System.Collections.Generic;

public interface ITurnPanelController
{

    void SetTiles();
    void UpdateQueue(ICharacterController ActiveCharacter, List<ICharacterController> RefreshedCharacters, List<ICharacterController> ExhaustedCharacters);
}
