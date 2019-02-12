using UnityEngine;

public class AbilitySoundEvent : AbstractEvent
{
    public AudioClip SoundEffect { get; }

    public AbilitySoundEvent(AudioClip soundEffect)
    {
        SoundEffect = soundEffect;
    }
}
