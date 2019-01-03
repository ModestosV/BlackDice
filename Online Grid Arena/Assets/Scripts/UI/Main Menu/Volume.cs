using UnityEngine;

public class Volume : MonoBehaviour
{
    private AudioSource AudioSource { get; set; }
    private float mainVolume = 0.15f;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        AudioSource = GetComponent<AudioSource>();
    }
	
	void Update()
    {
        if (AudioSource == null) return;

        AudioSource.volume = mainVolume;
    }

    public void SetVolume(float volume)
    {
        mainVolume = volume;
    }
}
