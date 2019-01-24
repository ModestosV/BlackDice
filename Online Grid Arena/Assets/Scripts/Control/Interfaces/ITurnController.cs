using System.Collections.Generic;

public interface ITurnController
{
    void SelectActiveCharacter();
    List<ICharacterController> GetLivingCharacters();
    bool IsActiveCharacter(ICharacterController character);
}
