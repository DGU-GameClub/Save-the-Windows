using System;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject settingMenu;

    bool IsPause = false;

    // void Start() {
    //     IsPause = false;
    // }

    // void Update(){
    //         if (IsPause == false) {
    //             Time.timeScale = 0;
    //             IsPause = true;
    //             return;
    //         }

    //         if (IsPause == true) {
    //             Time.timeScale = 1;
    //             IsPause = false;
    //             return;
    //         }
    // }

    public void pauseBtn()
    {
        if (IsPause)
        {
            Time.timeScale = 1;
            IsPause = false;
        }
        else
        {
            Time.timeScale = 0;
            IsPause = true;
        }
    }

    public void OnClickSettings()
    {
        //게임설정(배경음악, 기타음향 조절)
        settingMenu.SetActive(true);
        IsPause = true;
        Debug.Log("Settings");
    }
}
