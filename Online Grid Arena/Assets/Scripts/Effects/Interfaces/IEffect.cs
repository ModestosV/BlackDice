using System.Collections.Generic;

public interface IEffect
{
    EffectType Type { get; set; }
    bool IsDurationOver();
    void Refresh();
    void DecrementDuration();
    Dictionary<string, float> GetEffects();
    bool StacksRanOut();
    bool IsMaxStacks();
    void DecrementStack();
    string Print();
    string GetName();
    void Reset();
}