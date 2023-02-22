using System.Diagnostics;
using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Mathf함수 추가
using System;

//슬라이더랑 오디오믹서 추가
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public AudioSource bgmsource;
    public AudioSource sfxsource;

    //오디오 믹서
    public AudioMixer audioMixer;

    //슬라이더
    public Slider BgmSlider;
    public Slider SfxSlider;

    //menu
    public GameObject settingMenu;
    public GameObject beforeMenu;

    //오디오
    // public void SetBgmVolume(float volume)
    // {
    //     bgmsource.volume = volume;
    // }

    // public void SetSfxVolume(float volume)
    // {
    //     sfxsource.volume = volume;
    // }

    private void Start()
    {
        //초기화
        BgmSlider.value = PlayerPrefs.GetFloat("BGM", 0.50f);
        SfxSlider.value = PlayerPrefs.GetFloat("SFX", 0.50f);
        UnityEngine.Debug.Log("BgmValue is " + BgmSlider.value);

        DontDestroyOnLoad(gameObject);

    }

    //볼륨조절
    public void SetBgmSlider()
    {
        //UnityEngine.Debug.Log("BgmValue is " + BgmSlider.value);
        //로그 연산 값 오디오 믹서에 전달
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

    //setting menu에서 그전 menu로 이동
    public void OnClickBack()
    {
        settingMenu.SetActive(false);
        beforeMenu.SetActive(true);
    }

}