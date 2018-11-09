using System.Collections.Generic;

public interface ITurnTileController
{
    ICharacterController Character { set; }
    void updateTile(ICharacterController character);
}
