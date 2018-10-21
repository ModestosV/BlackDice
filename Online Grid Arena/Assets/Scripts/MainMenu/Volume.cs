using UnityEngine;

public class Volume : MonoBehaviour
{
    private AudioSource audioSource;
    private float mainVolume = 1f;

    void Awake ()
    {
        DontDestroyOnLoad(this.gameObject);
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
