using System.Collections.Generic;

public interface IEffect
{
    EffectType Type { get; set; }
    bool HasRunOut();
    void Refresh();
    void DecrementDuration();
    Dictionary<string, float> GetEffects();
    bool StacksRanOut();
    bool MaxStacks();
    void DecrementStack();
    string Print();
    string GetName();
    void Reset();
}