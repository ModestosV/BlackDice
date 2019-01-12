using UnityEngine;

public class AbilitySoundSource : MonoBehaviour, IEventSubscriber {

    private AudioSource audioSource;

    protected Texture abilityIcon;

    void Start ()
    {
        audioSource = GetComponent<AudioSource>();
        EventBus.Subscribe<AbilitySoundEvent>(this);
	}

    public void Handle(IEvent @event)
    {
        audioSource.PlayOneShot(((AbilitySoundEvent)@event).SoundEffect);
    }
}
