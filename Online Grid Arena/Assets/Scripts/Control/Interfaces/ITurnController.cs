using System.Collections.Generic;

public interface ITurnController
{
    void StartNextTurn();
    void AddCharacters(List<ICharacterController> characters);
    void AddCharacter(ICharacterController character);
    void RemoveCharacter(ICharacterController character);
    void SelectActiveCharacter();
}
