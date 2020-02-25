using UnityEngine.Audio;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public static AudioManager instance;
    void Awake()
    {
        if (instance == null)
            instance = this;
        else 
        {
            Destroy(gameObject); 
            return;
        }
        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    void Start()
    {
        if(PlayerPrefs.GetInt("GameMute") == 1)
            Mute();
        else
            UnMute();
        Play("BGMusic");
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s != null)
            s.source.Play();
    }

    public void Mute()
    {
        AudioListener.pause = true;
        PlayerPrefs.SetInt("GameMute", 1);
    }

    public void UnMute()
    {
        AudioListener.pause = false;
        PlayerPrefs.SetInt("GameMute", 0);
    }
}
