using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Mathf함수 추가
using System;

//슬라이더랑 오디오믹서 추가
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingMenu : MonoBehaviour
{
    // private GameObject[] musics;

    public static SettingMenu instance;

    public AudioSource bgmsource;
    public AudioSource sfxsource;

    //오디오 믹서
    public AudioMixer audioMixer;

    //슬라이더
    public Slider BgmSlider;
    public Slider SfxSlider;

    //menu
    public GameObject settingMenu;

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(instance);
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        //초기화
        BgmSlider.value = PlayerPrefs.GetFloat("BGM", 1f);
        SfxSlider.value = PlayerPrefs.GetFloat("SFX", 1f);
        UnityEngine.Debug.Log("BgmValue is " + BgmSlider.value);
    }

    //볼륨조절
    public void SetBgmSlider()
    {
        if(BgmSlider.value == -40f)
            audioMixer.SetFloat("BGM", -80f);
        else
        {
            audioMixer.SetFloat("BGM", MathF.Log10(BgmSlider.value) * 20);
        }
    }

    public void SetSfxSlider()
    {
        //로그 연산 값 오디오 믹서에 전달
        if(SfxSlider.value == -40f)
            audioMixer.SetFloat("SFX", -80f);
        else
            audioMixer.SetFloat("SFX", MathF.Log10(SfxSlider.value) * 20);
    }

    //sfx 확인용
    public void OnClickSfx()
    {
        sfxsource.Play();
    }
    //뒤로가기
    public void OnClickBack()
    {
        settingMenu.SetActive(false);
    }


    // //장면 전환시에 쓸수도 있으니 남겨두기
    // public void PlayMusic()
    // {
    //     if (audioSource.isPlaying) return;
    //     audioSource.Play();
    // }

    // public void StopMusic()
    // {
    //     audioSource.Stop();
    // }

}
