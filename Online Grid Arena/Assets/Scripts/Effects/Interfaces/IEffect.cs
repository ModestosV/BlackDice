﻿using System.Collections.Generic;

public interface IEffect
{
    EffectType Type { get; set; }
    bool HasRunOut();
    void Refresh();
    void Decrement();
    Dictionary<string, float> GetEffects();
    bool StacksRanOut();
    string Print();
    string GetName();
}