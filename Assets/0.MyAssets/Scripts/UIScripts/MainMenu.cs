using System.Runtime;
using System.Transactions;
using System;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainMenu : MonoBehaviour
{
    GameObject settingMenu;
    [SerializeField] float setTime = 10.0f;
    [SerializeField] TextMeshProUGUI countdownText;
    int min;
    float sec;
    void Start() 
    {
        settingMenu = GameObject.Find("SettingCanvas").transform.Find("Setting Menu").gameObject;
        countdownText.text = setTime.ToString();
    }

    void Update()
    {
        //타이머
        if (setTime > 0)
        {
            setTime -= Time.deltaTime;
            if (setTime >= 60f)
            {
                min = (int)setTime / 60;
                sec = setTime % 60;
                countdownText.text = "남은 시간 : " + min + "분 " + (int)sec + "초";
            }
            else if (setTime < 60f)
                countdownText.text = "남은 시간 : " + "0분 " + (int)setTime + "초";
        }

        if (setTime <= 0)
        {
            countdownText.text = "남은 시간 : 0분 0초";
            //몇초 쉬고 넘어가기
            GameOver();
        }
    }
    // public void pauseBtn()
    // {
    //     if (IsPause)
    //     {
    //         Time.timeScale = 1;
    //         IsPause = false;
    //     }
    //     else
    //     {
    //         Time.timeScale = 0;
    //         IsPause = true;
    //     }
    // }

    public void OnClickSettings()
    {
        //설정메뉴키기
        settingMenu.SetActive(true);
        Debug.Log("Settings");
    }

    public void GameOver(){
        //GameOver 캔버스 활성화
    }

}