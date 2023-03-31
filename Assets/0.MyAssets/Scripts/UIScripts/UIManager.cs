using System.ComponentModel;
using System.Data.SqlTypes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    GameObject settingMenu;
   
    public GameObject GameOver;
    public GameObject GameWin;
    
    [SerializeField] TextMeshProUGUI countdownText;
    [SerializeField] float setTime = 10.0f;
    int min;
    float sec;
    Store store;

    [SerializeField] TextMeshProUGUI moneyText;
    [SerializeField] TextMeshProUGUI lifeText;
    int money;
    int life;

    void Start() 
    {
        // settingMenu = GameObject.Find("Setting Canvas").transform.Find("Setting Menu").gameObject;
        countdownText.text = setTime.ToString();
        store = GameObject.Find("Store").GetComponent<Store>();
    }

    void Update()
    {
        //타이머 -> 정비시간때만 돌아가게. 실제 실행때는 리셋.
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
        }
        
        //돈
        if (money != GameManagers.instance.Money)
        {
            money = GameManagers.instance.Money;
            moneyText.text = ": " + money.ToString();
        }

        //생명
        if (life != GameManagers.instance.Life)
        {
            life = GameManagers.instance.Life;
            lifeText.text = ": " + life.ToString();
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
    }

    public void ResetStore()
    {
        if (store != null && GameManagers.instance.Money >= 20)
        {
            store.DestroyAllTower();
            //리셋버튼 클릭시 -20원으로 설정
            GameManagers.instance.Money -= 20;
        }
    }
}