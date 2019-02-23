using System.Collections.Generic;

public interface IPlayer
{
    void AddCharacterController(ICharacterController characterController);
    void RefreshCharacters();
    List<ICharacterController> GetUnusedCharacters();
}