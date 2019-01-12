using System.Collections.Generic;

public interface IEffect
{
    EffectType Type { get; set; }
    bool HasRunOut();
    void Refresh();
    void Decrement();
    Dictionary<string, float> GetEffects();
    bool StacksRanOut();
    bool MaxStacks();
    void RemoveStack();
    string Print();
    string GetName();
    void Reset();
}