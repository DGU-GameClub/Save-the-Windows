using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingCanvas : MonoBehaviour
{
    public static SettingCanvas instance;
    // Start is called before the first frame update
    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    private void Start() {
        instance.transform.GetChild(0).gameObject.SetActive(false);
    }
    public void OnClickInit()
    {
        SoundManager.instance.VolumeInit();
    }
    public void OnClickBack()
    {
        instance.transform.GetChild(0).gameObject.SetActive(false);
    }
}

