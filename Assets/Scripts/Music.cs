using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{

    public static Music foneMusic = null;
    public AudioClip music = null;
    AudioSource musicSource = null;

    void Start()
    {
        foneMusic = this;
        musicSource = gameObject.AddComponent<AudioSource>();
        musicSource.loop = true;
        musicSource.clip = music;
        if (SoundManager.manager.isMusicOn()) musicSource.Play();
    }

    public void turnOn()
    {
        musicSource.Play();
    }

    public void turnOff()
    {
        musicSource.Stop();
    }
}
