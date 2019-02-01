using System.Collections.Generic;

public interface ITurnController
{
    List<ICharacterController> GetLivingCharacters();
    bool IsActiveCharacter(ICharacterController character);
}
