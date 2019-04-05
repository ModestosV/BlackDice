using UnityEngine;
using System.Collections.Generic;

public class AudioController : MonoBehaviour
{
    [SerializeField] List<Object> songs;
    [SerializeField] List<Object> playedSongs;

    private AudioSource AudioSource { get; set; }
    private float mainVolume = 0.5f;

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
            if (songs.Count == 0)
            {
                songs.AddRange(playedSongs);
                playedSongs.Clear();
            }

            PlayNextSong();
        }

        AudioSource.volume = mainVolume;
    }

    public void SetVolume(float volume)
    {
        mainVolume = volume * 0.5f;
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
