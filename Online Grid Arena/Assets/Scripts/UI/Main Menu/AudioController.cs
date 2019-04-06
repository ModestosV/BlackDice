using UnityEngine;
using System.Collections.Generic;

public class AudioController : MonoBehaviour
{
    [SerializeField] List<Object> Songs;
    [SerializeField] List<Object> PlayedSongs;

    private AudioSource audioSource;
    private float mainVolume = 0.5f;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);

        audioSource = GetComponent<AudioSource>();

        Songs.AddRange(Resources.LoadAll("Audio/Music", typeof(AudioClip)));
        PlayNextSong();
    }
	
	void Update()
    {
        if (audioSource == null) return;
        else if (!audioSource.isPlaying)
        {
            if (Songs.Count == 0)
            {
                Songs.AddRange(PlayedSongs);
                PlayedSongs.Clear();
            }

            PlayNextSong();
        }

        audioSource.volume = mainVolume;
    }

    public void SetVolume(float volume)
    {
        mainVolume = volume * 0.5f;
    }

    private void PlayNextSong()
    {
        int next = Random.Range(0, Songs.Count);
        audioSource.clip = Songs[next] as AudioClip;
        audioSource.Play();
        PlayedSongs.Add(Songs[next]);
        Songs.RemoveAt(next);
    }
}
