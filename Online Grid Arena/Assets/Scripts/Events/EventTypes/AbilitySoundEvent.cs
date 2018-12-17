using UnityEngine;

public class AbilitySoundEvent : IEvent
{
    public AudioClip SoundEffect { get; }

    public AbilitySoundEvent(AudioClip soundEffect)
    {
        SoundEffect = soundEffect;
    }
}
