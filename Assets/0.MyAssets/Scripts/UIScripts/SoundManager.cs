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
    public static SoundManager instance;
    public AudioMixer audioMixer;
    public Slider BgmSlider;
    public Slider SfxSlider;
    [System.Serializable]
    public struct BgmType
    {
        public string name;
        public AudioClip audio;
    }

    // Inspector 에표시할 배경음악 목록
    public BgmType[] BGMList;

    public AudioSource BGM;
    private string NowBGMname = "";

    private void Awake() {
        if(instance != null)
        {
            Destroy(instance);
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        VolumeInit();
    }

    public void VolumeInit()
    {
        BgmSlider.value = PlayerPrefs.GetFloat("BGM", 0.5f);
        SfxSlider.value = PlayerPrefs.GetFloat("SFX", 0.5f);
    }
    //음악 재생
    public void PlayBGM(string name, bool isLoop, float Vol)
    {

        for (int i = 0; i < BGMList.Length; ++i)
            if (BGMList[i].name.Equals(name))
            {
                BGM.clip = BGMList[i].audio;
                BGM.loop = isLoop; 
                BGM.volume = Vol;
                BGM.Play();
                NowBGMname = name;
            }
    }
    public void StopBGM()
    {
        BGM.Stop();
    }
    //볼륨조절
    public void SetBgmSlider()
    {
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
}

