﻿using System.Collections.Generic;

public interface ITurnController
{
    List<ICharacter> RefreshedCharacters { get; set; }
    List<ICharacter> ExhaustedCharacters { get; set; }
    ICharacter ActiveCharacter { get; set; }

    void StartNextTurn();
    void EndTurn();
}