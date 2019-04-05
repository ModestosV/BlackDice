using UnityEngine;
using System.Collections.Generic;

public class AudioController : MonoBehaviour
{
    private List<Object> songs;
    private List<Object> playedSongs;

    private AudioSource AudioSource { get; set; }
    private float mainVolume = 0.15f;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        AudioSource = GetComponent<AudioSource>();

        songs.AddRange(Resources.LoadAll("Audio/Music", typeof(AudioClip)));
        PlayNextSong();
    }
	
	void Update()
    {
        if (AudioSource == null) return;
        else if (!AudioSource.isPlaying)
        {
            PlayNextSong();

            if (songs[0] == null)
            {
                songs.AddRange(playedSongs);
                playedSongs.Clear();
            }
        }

        AudioSource.volume = mainVolume;
    }

    public void SetVolume(float volume)
    {
        mainVolume = volume;
    }

    private void PlayNextSong()
    {
        int next = Random.Range(0, songs.Count);
        AudioSource.clip = songs[next] as AudioClip;
        AudioSource.Play();
        playedSongs.Add(songs[next]);
        songs.RemoveAt(next);
    }
}
