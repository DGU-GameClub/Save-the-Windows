using System;
using UnityEngine;

using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundOptions : MonoBehaviour
{
    //오디오 믹서
    public AudioMixer audioMixer;

    //슬라이더
    public Slider BgmSlider;
    public Slider SfxSlider;

    //볼륨조절
    public void SetBgmVolume()
    {
        //로그 연산 값 오디오 믹서에 전달
        audioMixer.SetFloat("BGM", MathF.Log10(BgmSlider.value) * 20);
    }

    public void SetSfxVolume()
    {
        //로그 연산 값 오디오 믹서에 전달
        audioMixer.SetFloat("SFX", MathF.Log10(SfxSlider.value) * 20);
    }
}
