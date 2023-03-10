using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    GameObject settingMenu;
    GameManager gameManager;
    public GameObject GameOver;
    public GameObject GameWin;
    
    [SerializeField] TextMeshProUGUI countdownText;
    [SerializeField] float setTime = 10.0f;
    int min;
    float sec;

    void Start() 
    {
        // settingMenu = GameObject.Find("Setting Canvas").transform.Find("Setting Menu").gameObject;
        countdownText.text = setTime.ToString();
        gameManager = FindObjectOfType<GameManager>();
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
            gameManager.GameOver();
        }
    }

    public void OnClickSettings()
    {
        //설정메뉴키기
        settingMenu.SetActive(true);
        Debug.Log("Settings");
    }

    public void GameOverOn()
    {
        GameOver.SetActive(true);
    }

    public void GameWinOn()
    {
        GameWin.SetActive(true);
        //소리 넣기
    }
}