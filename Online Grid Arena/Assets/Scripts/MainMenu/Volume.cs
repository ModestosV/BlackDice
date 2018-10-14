using UnityEngine;

public class Volume : MonoBehaviour
{
    private AudioSource audioSource;
    private float mainVolume = 1f;

    void Start ()
    {
        audioSource = GetComponent<AudioSource>();
    }
	
	void Update ()
    {
        audioSource.volume = mainVolume;
    }

    public void SetVolume(float volume)
    {
        mainVolume = volume;
    }
}
