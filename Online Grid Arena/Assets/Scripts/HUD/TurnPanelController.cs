using System.Collections.Generic;

public class TurnPanelController : ITurnPanelController
{ 
    public ITurnController TurnController { get; set; }

    public List<ICharacter> Characters { get; set; } // may be unnecessary

    public void updateQueue()
    {
        
    }
}
