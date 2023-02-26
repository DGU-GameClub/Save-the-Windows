using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    //오디오 소스
    private AudioSource audioSource;
    private GameObject[] musics;
    
    private void Awake() {
        musics = GameObject.FindGameObjectsWithTag("Music");

        //배경음악 한번만 틀어주기(중복 제거)
        if(musics.Length >= 2)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(gameObject);
        audioSource = GetComponent<AudioSource>();
    }

    //장면 전환시에 쓸수도 있으니 남겨두기
    public void PlayMusic()
    {
        if (audioSource.isPlaying) return;
        audioSource.Play();
    }

    public void StopMusic()
    {
        audioSource.Stop();
    }

}
