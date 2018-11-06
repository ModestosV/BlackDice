using UnityEngine;

public class Volume : MonoBehaviour
{
    private AudioSource audioSource;
    private float mainVolume = 0.15f;

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        audioSource = GetComponent<AudioSource>();
    }
	
	void Update()
    {
        if (audioSource == null) return;

        audioSource.volume = mainVolume;
    }

    public void SetVolume(float volume)
    {
        mainVolume = volume;
    }
}
