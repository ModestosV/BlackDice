using System.Collections.Generic;

public interface ITurnController
{
    List<ICharacterController> RefreshedCharacters { set; }
    List<ICharacterController> ExhaustedCharacters { set; }
    ICharacterController ActiveCharacter { set; }

    IEndMatchPanel EndMatchPanel { set; }

    void StartNextTurn();
    void AddCharacters(List<ICharacterController> characters);
    void AddCharacter(ICharacterController character);
    void RemoveCharacter(ICharacterController character);
    void SelectActiveCharacter();
    void CheckWinCondition();
    void Surrender();
    List<ICharacterController> GetLivingCharacters();
    bool IsActiveCharacter(ICharacterController character);
}
