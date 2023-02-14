using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource bgmsource;

    public AudioSource sfxsource;

    public void SetBgmVolume(float volume)
    {
        bgmsource.volume = volume;
    }

    public void SetSfxVolume(float volume)
    {
        sfxsource.volume = volume;
    }

    //sfx 확인용
    public void OnSfx()
    {
        sfxsource.Play();
    }
}
