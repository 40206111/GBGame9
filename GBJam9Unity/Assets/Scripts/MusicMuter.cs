using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicMuter : MonoBehaviour
{
    AudioSource Music;
    float OriginalVolume = 0.5f;

    private void Awake()
    {
        Music = GetComponent<AudioSource>();
        OriginalVolume = Music.volume;
        PlayMusic(SettingsControl.PlayMusic);
    }

    public void PlayMusic(bool playing)
    {
        Music.volume = OriginalVolume * (playing ? 1.0f : 0.0f);
    }

    private void OnEnable()
    {
        SettingsControl.PlayMusicSetEvent += PlayMusic;
    }
    private void OnDisable()
    {
        SettingsControl.PlayMusicSetEvent -= PlayMusic;
    }
}
