using System.Collections.Generic;
using UnityEngine;

public interface IEffect
{
    EffectType Type { get; set; }
    string Description { get; }
    Sprite EffectIcon { get; set; }
    int DurationRemaining { get; }

    bool IsDurationOver();
    void Refresh();
    void DecrementDuration();
    Dictionary<string, float> GetEffects();
    bool StacksRanOut();
    bool IsMaxStacks();
    void DecrementStack();
    string GetName();
    void Reset();
}